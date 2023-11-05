using UnityEngine;

public class DefaultCore : MonoBehaviour
{
    [SerializeField] private float _power = 1.0f;
    [SerializeField] private float _lifeTime = 1.0f;

    [Tooltip("Список эффектов: Нулевой - остальное; Первый - вода; Второй - враги")]
    [SerializeField] private GameObject[] _hitsFX;

    private Rigidbody _rigidBody;

    private OceanShaderParameters _oceanParameters;

    void Awake()
    {
        _oceanParameters = FindObjectOfType<OceanShaderParameters>();
        GameObject Parent = GameObject.Find("Projectiles");

        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidBody.AddForce(gameObject.transform.forward * _power, ForceMode.VelocityChange);
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        OnWaterKiller();
    }

    // Столкновение с водой
    void OnWaterKiller()
    {
        if (_oceanParameters == null)
            return;

        if (transform.position.y <= getHeightAtPosition(transform.position))
        {
            Instantiate(_hitsFX[1], transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }
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

        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(gameObject, 10);
        }

        Destroy(gameObject);
    }

    public void SetSpeed(float newSpeed)
    {
        _power = newSpeed;
    }

    public float getHeightAtPosition(Vector3 position)
    {
        float time = Time.time;
        Vector3 currentPosition = GetAdditionWaveHeight(position, time);
        Vector3 diff = new Vector3(position.x - currentPosition.x, 0, position.z - currentPosition.z);
        currentPosition = GetAdditionWaveHeight(diff, time);
        return currentPosition.y;
    }

    public Vector3 GetAdditionWaveHeight(Vector3 position, float timeSinceStart)
    {
        Vector3 result = new Vector3();
        result = GerstnerWaveGenerator(position, _oceanParameters.DirectionWave[0], _oceanParameters.StepnessWave[0], _oceanParameters.LenghtWave[0], _oceanParameters.SpeedWave[0], timeSinceStart) +
            GerstnerWaveGenerator(position, _oceanParameters.DirectionWave[1], _oceanParameters.StepnessWave[1], _oceanParameters.LenghtWave[1], _oceanParameters.SpeedWave[1], timeSinceStart) +
            GerstnerWaveGenerator(position, _oceanParameters.DirectionWave[2], _oceanParameters.StepnessWave[2], _oceanParameters.LenghtWave[2], _oceanParameters.SpeedWave[2], timeSinceStart) +
            GerstnerWaveGenerator(position, _oceanParameters.DirectionWave[3], _oceanParameters.StepnessWave[3], _oceanParameters.LenghtWave[3], _oceanParameters.SpeedWave[3], timeSinceStart);
        return result;
    }

    /// <summary>
    /// Generator Gerstner waves
    /// </summary>
    /// <returns>Position Vertex in current place</returns>
    public Vector3 GerstnerWaveGenerator(Vector3 position, float angle, float steepness, float wavelength, float speed, float timeSinceStart)
    {
        float k = 2 * Mathf.PI / wavelength;

        Vector3 basedDirection = new Vector3(1, 1, 0);
        Vector3 normilizedDirection = basedDirection.RotateAroundAxis(Vector3.forward, angle);

        float f = k * Vector2.Dot(normilizedDirection, new Vector2(position.x, position.z)) - (speed * timeSinceStart);
        float a = steepness / k;

        return new Vector3(normilizedDirection.x * a * Mathf.Cos(f), a * Mathf.Sin(f), normilizedDirection.y * a * Mathf.Cos(f));
    }
}
