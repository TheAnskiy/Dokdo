using System.Collections;
using UnityEngine;

public class DefaultCore : MonoBehaviour
{
    [SerializeField] private float _power = 1.0f;
    [SerializeField] private float _lifeTime = 1.0f;
    
    [Tooltip("Список эффектов: Нулевой - остальное; Первый - столкновение с водой; Второй - эффект под водой; Третий - враги")]
    [SerializeField] private GameObject[] _hitsFX;

    private bool _hitWater = false;
    private Rigidbody _rigidBody;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.AddForce(gameObject.transform.forward * _power, ForceMode.VelocityChange);
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        Debug.Log(transform.eulerAngles);
        OnWaterKiller();
    }


    // Столкновение с водой
    void OnWaterKiller()
    {
        if (transform.position.y <= OceanShaderParameters.Instance.GetHeightAtPosition(transform.position))
        {
            if (!_hitWater)
            {
                // Спавн столкновения с водой
                Instantiate(_hitsFX[1], transform.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x - 65, transform.eulerAngles.y, transform.eulerAngles.z)));
                StartCoroutine(SpawnFx(_hitsFX[2]));
                Destroy(gameObject, 0.5f);
            }
            _hitWater = true;
        }
    }

    // Подводный эффект
    private IEnumerator SpawnFx(GameObject FX)
    {
        while (true)
        {
            Instantiate(FX, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Произошло столкновение
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("Water"):
                Instantiate(_hitsFX[2], transform.position, Quaternion.identity);
                break;
            case ("Enemy"):
                Instantiate(_hitsFX[3], transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(_hitsFX[0], transform.position, Quaternion.identity);
                break;
        }
        Destroy(gameObject);
    }

    public void SetSpeed(float newSpeed)
    {
        _power = newSpeed;
    }
}
