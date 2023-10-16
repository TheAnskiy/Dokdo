using UnityEngine;

public class Buoy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;

    [HideInInspector] public float _strengeFloating = 3;
    [HideInInspector] public float _depthBeforeSubmerged = 1.5f;

    private OceanShaderParameters _oceanParameters;
    
    // Отладочные параметры:
    private float _countBuoy = 1f;

    private float _waveHeight;

    void Awake()
    {
        _oceanParameters = FindObjectOfType<OceanShaderParameters>();
    }

    void Update()
    {
        // Init gravity to buoy
        _rigidBody.AddForceAtPosition(Physics.gravity / _countBuoy, transform.position, ForceMode.Acceleration);

        _waveHeight = getHeightAtPosition(transform.position);
        if (transform.position.y < _waveHeight)
            Floating();
    }

    /// <summary>
    /// The function applies an ejection force to the object, like water
    /// </summary>
    void Floating()
    {
        float displacementMultiplier = Mathf.Clamp01((_waveHeight - transform.position.y) / _depthBeforeSubmerged) * _strengeFloating;
        _rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

        //_rigidBody.AddForce(/*displacementMultiplier * */-_rigidBody.velocity * Time.deltaTime, ForceMode.VelocityChange);
        //_rigidBody.AddTorque(/*displacementMultiplier * */-_rigidBody.angularVelocity * Time.deltaTime, ForceMode.VelocityChange);
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
    public Vector3 GerstnerWaveGenerator(Vector3 position, Vector2 direction, float steepness, float wavelength, float speed, float timeSinceStart)
    {
        float k = 2 * Mathf.PI / wavelength;

        Vector2 normilizedDirection = direction.normalized;

        float f = k * Vector2.Dot(normilizedDirection, new Vector2(position.x, position.z)) - (speed * timeSinceStart);
        float a = steepness / k;

        return new Vector3(normilizedDirection.x * a * Mathf.Cos(f), a * Mathf.Sin(f), normilizedDirection.y * a * Mathf.Cos(f));
    }
}
