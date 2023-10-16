using UnityEngine;

public class ShipControllerPhys : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Main Ship Physic:")]
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _rotateForce;

    [SerializeField] private float _maxAngular = 1f;

    private float _rotateX = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = _maxAngular;
    }

    void Update()
    {
        // Перемещение обьекта:
        if (Input.GetKey(KeyCode.W))
            ObjectPhysMover(transform.forward);
        if (Input.GetKey(KeyCode.S))
            ObjectPhysMover(-transform.forward);

        // Вращение вправо-влево:
        if (Input.GetKey(KeyCode.A))
            ObjectPhysRotator(-transform.up);
        if (Input.GetKey(KeyCode.D))
            ObjectPhysRotator(transform.up);
    }


    /// <summary>
    /// Функция реализации перемещения обьекта по физике
    /// </summary>
    private void ObjectPhysMover(Vector3 axis)
    {
        _rigidbody.AddForce(axis * _forwardForce);
    }


    /// <summary>
    /// Функция реализации вращения обьекта по физике
    /// </summary>
    private void ObjectPhysRotator(Vector3 axis)
    {
        _rigidbody.AddTorque(axis * _rotateForce);
    }
}