using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private InputController _controller;

    [Header("Main Ship Movement:")]
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _maxDashForce;
    [SerializeField] private float _accelerationStep;
    [Space(10)]
    [SerializeField] private float _rotateForce;

    private float _acceleration;
    private float _maxAngular = 1f;

    // Offset Y position centre of mass
    [SerializeField] private float _offset = 2;

    private void Awake()
    {
        _acceleration = _forwardForce;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = _maxAngular;
        _controller = GetComponent<InputController>();
    }

    void Update()
    {
        // Checking the wave height
        float _waveHeight = OceanShaderParameters.Instance.GetHeightAtPosition(transform.position);
        if (_rigidbody.worldCenterOfMass.y - _offset <= _waveHeight)
        {
            _rigidbody.drag = 1f;
        }
        else
        {
            _rigidbody.drag = 0.2f;
        }

        // Accumulation of acceleration
        if (_controller.dash)
            Acceleration(1f, _accelerationStep);
        else
            Acceleration(-1f, _accelerationStep);

        // Moving an object
        ObjectPhysMover(new Vector3(_controller.inputMoveble.x, 0, _controller.inputMoveble.z));
        // Rotate an object
        ObjectPhysRotator(new Vector3(0, _controller.inputMoveble.y, 0));
    }

    private void Acceleration(float multiplier, float step)
    {
        if ((_acceleration < _maxDashForce) && (multiplier > 0))
            _acceleration += (multiplier * step * Time.deltaTime);
        if ((_acceleration > _forwardForce) && (multiplier < 0))
            _acceleration += (multiplier * step * Time.deltaTime);
    }

    private void ObjectPhysMover(Vector3 axis)
    {
        _rigidbody.AddForce(axis * _acceleration * Time.deltaTime);
    }

    private void ObjectPhysRotator(Vector3 axis)
    {
        _rigidbody.AddTorque(axis * _rotateForce * Time.deltaTime);
    }
}