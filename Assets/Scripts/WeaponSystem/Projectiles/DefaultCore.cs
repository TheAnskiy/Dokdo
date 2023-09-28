using System.Collections.Generic;
using UnityEngine;

public class DefaultCore : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _lifeTime = 1.0f;

    [Tooltip("Список эффектов: Нулевой - остальное; Первый - вода; Второй - враги")]
    [SerializeField] private GameObject[] _hitsFX;

    private Rigidbody _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.AddForce(gameObject.transform.forward * _speed, ForceMode.VelocityChange);

        Destroy(gameObject, _lifeTime);
    }

    // Произошло столкновение
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("Water"):
                Instantiate(_hitsFX[1], transform.position, Quaternion.identity);
                break;
            case ("Enemy"):
                Instantiate(_hitsFX[2], transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(_hitsFX[0], transform.position, Quaternion.identity);
                break;
        }
        Destroy(gameObject);
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

}
