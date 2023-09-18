using UnityEngine;

public class ShipControllerPhys : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Main Ship Physic:")]
    [SerializeField] private float _forwardForce;
    [SerializeField] private float _rotateForce;

    [Header("Main Ship rotation:")]
    [Range(1f, 50f)][SerializeField] private float _rotateSpeed = 1f;

    private float _rotateX = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Перемещение обьекта:
        if (Input.GetKey(KeyCode.W))
            ObjectPhysMover(-transform.right);
        if (Input.GetKey(KeyCode.S))
            ObjectPhysMover(transform.right);

        //// Вращение вправо-влево:
        //if (Input.GetKey(KeyCode.A))
        //    ObjectRotator(1);
        //if (Input.GetKey(KeyCode.D))
        //    ObjectRotator(-1);

        // Вращение вправо-влево:
        if (Input.GetKey(KeyCode.A))
            ObjectPhysRotator(transform.forward);
        if (Input.GetKey(KeyCode.D))
            ObjectPhysRotator(-transform.forward);
    }


    /// <summary>
    /// Функция реализации перемещения по физике
    /// </summary>
    private void ObjectPhysMover(Vector3 axis)
    {
        _rigidbody.AddRelativeForce(axis * _forwardForce);
    }


    /// <summary>
    /// Функция реализации вращения камеры
    /// </summary>
    private void ObjectPhysRotator(Vector3 axis)
    {
        _rigidbody.AddTorque(axis * _rotateForce);
    }

    //private void ObjectRotator(int directionMultiplier)
    //{
    //    _rotateX += directionMultiplier * _rotateSpeed * Time.deltaTime;
    //    gameObject.transform.у += _rotateX;
    //}
}