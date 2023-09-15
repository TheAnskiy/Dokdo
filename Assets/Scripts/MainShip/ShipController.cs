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
    private float x = 0f;

    void Update()
    {
        // Накопление ускорения:
        AccelerationCalculate();
        // Перемещение обьекта
        ObjectMover();

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
    /// Функция реализации перемещения камеры
    /// </summary>
    private void ObjectMover()
    {
        gameObject.transform.position += gameObject.transform.forward * _movementAcceleration * Time.deltaTime;
    }

    /// <summary>
    /// Функция реализации вращения камеры
    /// </summary>
    private void ObjectRotator(int directionMultiplier)
    {

        if (_movementAcceleration >= 0)
        {
            x += directionMultiplier * _rotateSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = new Vector3(0f, x, 0f);
        }
        if (_movementAcceleration < 0)
        {
            x += -directionMultiplier * _rotateSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = new Vector3(0f, x, 0f);
        }
    }
}