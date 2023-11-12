using UnityEngine;
using System;

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

    [SerializeField] private State _state = State.Wait;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _patrolPoints;
    private int _curIndexPoint;

    [field: SerializeField] public float Health { get; private set; } = 100.0f;
    public float MaxHealth { get; private set; }
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private float _startDistance = 100;
    [SerializeField] private float _acceleration = 6000.0f;

    [SerializeField] private PID _pidForRotation;

    private Rigidbody _rb;
    private PathFinder _pathFinder;

    private bool _startTimer = false;
    private float _timerInverted = 0.0f;
    private float _lifeTimeInverted = 5.0f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathFinder = GetComponentInChildren<PathFinder>();

        MaxHealth = Health;

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

    private void FixedUpdate()
    {
        KillInverted();

        if (_target == null)
            return;

        // TODO: Сделать машину состояний.
        switch (_state)
        {
            case State.Wait:

                if ((_target.position - transform.position).sqrMagnitude > _startDistance * _startDistance)
                    break;

                ToState(State.MoveToTarget);

                // Activate Cannon
                Cannon[] cannons = GetComponentsInChildren<Cannon>();
                for (int i = 0; i < cannons.Length; i++)
                {
                    cannons[i].enabled = true;
                    cannons[i].SetTarget(_target);
                }

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
            case State.Patrolling: _pathFinder.SetTarget(_patrolPoints[_curIndexPoint]); return;
            default: return;
        }
    }

    private void KillInverted()
    {
        if (IsInverted())
        {
            if (_startTimer == false)
                _startTimer = true;

            _timerInverted -= Time.fixedDeltaTime;

            if (_timerInverted < -_lifeTimeInverted)
                Die();
        }
        else if (_startTimer == true)
        {
            _startTimer = false;
            _timerInverted = 0.0f;
        }
    }

    private bool IsInverted()
    {
        return Vector3.Dot(Vector3.up, transform.up) < -0.5f;
    }

    public void TakeDamage(GameObject damageCauser, float damageAmount)
    {
        Health -= damageAmount;
        OnHealthChanged?.Invoke(MaxHealth, Health);

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _startDistance, 5);

        if (_patrolPoints != null)
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
