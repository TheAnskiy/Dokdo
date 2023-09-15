using System.Collections;
using UnityEngine;

public class DefaultCore : MonoBehaviour
{
    [SerializeField] public float _shotImpulse = 1;
    [Tooltip("Список эффектов: Нулевой - остальное; Первый - вода; Второй - враги")]
    [SerializeField] private GameObject[] _hitsFX;
    private GameObject Parent;
    private Rigidbody _rigidBody;

    void Awake()
    {
        Parent = GameObject.Find("Projectiles");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.AddForce(gameObject.transform.right * _shotImpulse, ForceMode.Impulse);
        StartCoroutine(Killer());
    }

    // Не произошло столкновения
    IEnumerator Killer()
    {
        gameObject.transform.SetParent(Parent.transform, true);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
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
}
