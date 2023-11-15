using UnityEngine;
using System.Collections;
// using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TestAI : MonoBehaviour, IDamageable
{
    enum State
    {
        Wait,
        MoveToTarget,
        Patrolling,
    }

    [Header("AI")]
    [SerializeField] private State _state = State.Wait;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _patrolPoints;
    private int _curIndexPoint;


    [SerializeField] private float _startDistance = 100;
    [SerializeField] private float _distanceDeath = 500;

    [field: Space(20)]
    [field: SerializeField] public float Health { get; private set; } = 100.0f;
    public float MaxHealth { get; private set; }
    public event System.Action<float, float> OnHealthChanged;
    [SerializeField] private float _acceleration = 6000.0f;

    [SerializeField] private PID _pidForRotation;

    [SerializeField] private GameObject[] _lootAfterDeath;

    private Rigidbody _rb;
    private PathFinder _pathFinder;
    private BuoyController _buoyController;

    private bool _isInverted = false;
    private float _timerInverted = 0.0f;
    private float _lifeTimeInverted = 5.0f;

    private bool _isDead = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathFinder = GetComponentInChildren<PathFinder>();
        _buoyController = GetComponentInChildren<BuoyController>();

        MaxHealth = Health;

        if (_target == null)
            _target = Global.Instance.MainShip.transform;

        switch (_state)
        {
            case State.Wait:
                break;
            case State.MoveToTarget:
                _pathFinder.SetTarget(_target);
                break;
            case State.Patrolling:
                _pathFinder.SetTarget(_patrolPoints[0]);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        Cannon[] cannons = GetComponentsInChildren<Cannon>();
        for (int i = 0; i < cannons.Length; i++)
            cannons[i].SetTarget(_target);
    }

    private void FixedUpdate()
    {
        KillInverted();

        if (_target == null || _isDead || _isInverted || _target.IsInverted())
            return;
        

        if ((_target.position - transform.position).sqrMagnitude > _distanceDeath * _distanceDeath)
        {
            Destroy(gameObject);
        }

        // TODO: Сделать машину состояний.
        switch (_state)
        {
            case State.Wait:

                if ((_target.position - transform.position).sqrMagnitude > _startDistance * _startDistance)
                    break;

                ToState(State.MoveToTarget);

                // Activate Cannon
                

                break;

            case State.MoveToTarget:

                if ((_target.position - transform.position).sqrMagnitude > _startDistance * _startDistance)
                {
                    ToState(State.Patrolling);
                    break;
                }

                MoveToTarget();
                break;

            case State.Patrolling:

                if ((_target.position - transform.position).sqrMagnitude < _startDistance * _startDistance)
                {
                    ToState(State.MoveToTarget);
                    break;
                }

                MoveToTarget();

                if ((_patrolPoints[_curIndexPoint].position - transform.position).sqrMagnitude < 10 * 10)
                {
                    _curIndexPoint += 1;
                    if (_curIndexPoint >= _patrolPoints.Length)
                        _curIndexPoint = 0;

                    _pathFinder.SetTarget(_patrolPoints[_curIndexPoint]);
                }

                break;

            default:
                break;
        }
    }

    private void MoveToTarget()
    {
        if (_pathFinder.Path.corners.Length < 2) // Если оибка в определении пути, ничего неделаем.
            return;

        Vector3 targetDirection = -transform.position + (_pathFinder.Path.corners[1]);
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float yAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation.eulerAngles.y);
        float yTorqueCorrection = _pidForRotation.GetOutput(yAngleError, Time.fixedDeltaTime);

        _rb.AddTorque(yTorqueCorrection * transform.up);
        _rb.AddForce(transform.forward * _acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    private void ToState(State newState)
    {
        _state = newState;
        switch (_state)
        {
            case State.Wait: return;
            case State.MoveToTarget: _pathFinder.SetTarget(_target); return;
            case State.Patrolling:
                if (_patrolPoints?.Length <= 0)
                {
                    ToState(State.Wait);
                    break;
                }
                _pathFinder.SetTarget(_patrolPoints[_curIndexPoint]); 
                return;
            default: return;
        }
    }

    private void KillInverted()
    {
        if (transform.IsInverted())
        {
            if (_isInverted == false)
            {
                _isInverted = true;
            }

            _timerInverted -= Time.fixedDeltaTime;

            if (_timerInverted < -_lifeTimeInverted && _isDead == false)
            {
                StartCoroutine(Die());
            }
        }
        else if (_isInverted == true)
        {
            _isInverted = false;
            _timerInverted = 0.0f;
        }
    }

    public void TakeDamage(GameObject damageCauser, float damageAmount)
    {
        Health -= damageAmount;
        OnHealthChanged?.Invoke(MaxHealth, Health);

        if (Health <= 0)
            StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        _isDead = true;

        float oldForce = _buoyController._floatingStrenge;
        float newForce = 0;
        float _time = 0f;
        while (_time < 1)
        {
            _time += 0.5f * Time.deltaTime;
            _buoyController.SetFloatingParameters(Mathf.Lerp(oldForce, newForce, _time), _buoyController._depth);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        SpawnLoot();
        Destroy(gameObject);
    }


    private void SpawnLoot()
    {
        if (_lootAfterDeath?.Length <= 0)
            return;

        foreach (GameObject loot in _lootAfterDeath)
        {
            Vector3 randPos = new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y, transform.position.z + Random.Range(-5f, 5f));
            Instantiate(loot, randPos, Quaternion.identity);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _startDistance, 5);

        Handles.color = Color.black;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceDeath, 5);

        if (_patrolPoints?.Length > 0)
        {
            Handles.color = Color.green;
            Handles.DrawLine(transform.position, _patrolPoints[0].position);
            Handles.DrawLine(_patrolPoints[_patrolPoints.Length - 1].position, _patrolPoints[0].position);
            for (int i = 0; i < _patrolPoints.Length - 1; ++i)
                Handles.DrawLine(_patrolPoints[i].position, _patrolPoints[i + 1].position);

        }
    }
#endif
}
