using System.Collections;
using UnityEngine;

public class TestAI : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float Health { get; private set; } = 100.0f;

    [SerializeField] private Transform _target;
    [SerializeField] private float _startDistance = 100;

    [SerializeField] private float _acceleration = 6000.0f;


    [SerializeField] private PID _pidForRotation;

    private Rigidbody _rb;
    private PathFinder _pathFinder;

    private bool _isStarted = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pathFinder = GetComponentInChildren<PathFinder>();
        _pathFinder.SetTarget(_target);
    }


    private void FixedUpdate()
    {
        if (_isStarted == false)
        {
            if ((_target.position - transform.position).sqrMagnitude < _startDistance * _startDistance)
            {
                _isStarted = true;

                // Activate Cannon
                Cannon[] cannons = GetComponentsInChildren<Cannon>();
                for (int i = 0; i < cannons.Length; i++)
                {
                    cannons[i].enabled = true;
                    cannons[i].SetTarget(_target);
                }
            }

            return;
        }

        if (_pathFinder.Path == null)
            return;

        if (_pathFinder.Path.corners.Length < 2)
            return;

        Vector3 targetDirection = -transform.position + (_pathFinder.Path.corners[1]);
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float yAngleError = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetRotation.eulerAngles.y);
        float yTorqueCorrection = _pidForRotation.GetOutput(yAngleError, Time.fixedDeltaTime);

        _rb.AddTorque(yTorqueCorrection * transform.up);
        _rb.AddForce(transform.forward * _acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public void TakeDamage(GameObject damageCauser, float damageAmount)
    {
        Health -= damageAmount;

        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
