using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Main Ship movement:")]
    [SerializeField] private float _movementMaxSpeed = 15f;
    private float _movementAcceleration = 0f;
    [Range(0.1f, 100f)][SerializeField] private float _movementAccelerationUp = 1f;
    [Range(0.1f, 100f)][SerializeField] private float _movementAccelerationDown = 1f;

    [Header("Main Ship rotation:")]
    [Range(1f, 50f)][SerializeField] private float _rotateSpeed = 1f;
    
    private float _rotateX = 0f;
    [Header("Main Ship Physic:")]
    private Rigidbody _rigidbody;
    [SerializeField] private float _forwardForce;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Накопление ускорения:
        AccelerationCalculate();
        // Перемещение обьекта
        //ObjectMover();

        if (Input.GetKey(KeyCode.W))
            ObjectPhysMover(-transform.right);
        if (Input.GetKey(KeyCode.S))
            ObjectPhysMover(transform.right);


        // Вращение вправо-влево:
        if (Input.GetKey(KeyCode.A))
            ObjectRotator(-1);
        if (Input.GetKey(KeyCode.D))
            ObjectRotator(1);
    }

    /// <summary>
    /// Накопление и сброс ускорения для перемещения
    /// </summary>
    private void AccelerationCalculate()
    {
        if ((Input.GetKey(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.RightShift)))
            _movementAcceleration = _movementMaxSpeed;
        if ((Input.GetKey(KeyCode.LeftControl)) || (Input.GetKey(KeyCode.RightControl)))
            _movementAcceleration = -_movementMaxSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            _movementAcceleration += _movementAccelerationUp * Time.deltaTime;
            _movementAcceleration = Mathf.Clamp(_movementAcceleration, -_movementMaxSpeed, _movementMaxSpeed);
        }
        else if (_movementAcceleration > 0)
        {
            _movementAcceleration -= _movementAccelerationDown * Time.deltaTime;
            _movementAcceleration = Mathf.Clamp(_movementAcceleration, 0, _movementMaxSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _movementAcceleration -= _movementAccelerationUp * Time.deltaTime;
            _movementAcceleration = Mathf.Clamp(_movementAcceleration, -_movementMaxSpeed, _movementMaxSpeed);
        }
        else if (_movementAcceleration < 0)
        {
            _movementAcceleration += _movementAccelerationDown * Time.deltaTime;
            _movementAcceleration = Mathf.Clamp(_movementAcceleration, -_movementMaxSpeed, 0);
        }
    }

    /// <summary>
    /// Функция реализации перемещения по трансформу
    /// </summary>
    private void ObjectMover()
    {
        gameObject.transform.position += gameObject.transform.forward * _movementAcceleration * Time.deltaTime;
    }

    /// <summary>
    /// Функция реализации перемещения по физике
    /// </summary>
    private void ObjectPhysMover(Vector3 axis)
    {
        _rigidbody.AddForce(axis * _forwardForce);
    }

    /// <summary>
    /// Функция реализации вращения камеры
    /// </summary>
    private void ObjectRotator(int directionMultiplier)
    {

        if (_movementAcceleration >= 0)
        {
            _rotateX += directionMultiplier * _rotateSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = new Vector3(0f, _rotateX, 0f);
        }
        if (_movementAcceleration < 0)
        {
            _rotateX += -directionMultiplier * _rotateSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = new Vector3(0f, _rotateX, 0f);
        }
    }
}