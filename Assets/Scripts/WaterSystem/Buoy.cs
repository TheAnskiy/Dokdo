using UnityEngine;

public class Buoy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] OceanShaderParameters _oceanParameters;

    [Header("Damping factors:")]
    [SerializeField] private float _buoyDrag = 1f;
    [SerializeField] private float _buoyAngularDrag = 12f;

    [Header("Buoy settings:")]
    [SerializeField] private float _countBuoy = 1f;
    [SerializeField] private float _depthBeforeSubmerged = 1f;
    [SerializeField] private float _strengeFloating = 3;

    private float _waveHeight;

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

        _rigidBody.AddForce(displacementMultiplier * -_rigidBody.velocity * _buoyDrag * Time.deltaTime, ForceMode.VelocityChange);
        _rigidBody.AddTorque(displacementMultiplier * -_rigidBody.angularVelocity * _buoyAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
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
        result = GerstnerWaveGenerator(position, _oceanParameters.DirectionBasedWave, _oceanParameters.StepnessBasedWave, _oceanParameters.LenghtBasedWave, _oceanParameters.SpeedBasedWave, timeSinceStart) +
            GerstnerWaveGenerator(position, _oceanParameters.DirectionMiddleWave, _oceanParameters.StepnessMiddleWave, _oceanParameters.LenghtMiddleWave, _oceanParameters.SpeedMiddleWave, timeSinceStart) +
            GerstnerWaveGenerator(position, _oceanParameters.DirectionMicroWave, _oceanParameters.StepnessMicroWave, _oceanParameters.LenghtMicroWave, _oceanParameters.SpeedMicroWave, timeSinceStart);
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

    /// <summary>
    /// Generator Harmonic waves
    /// </summary>
    /// <returns>Waves height in current position</returns>
    public float GetHeightHarmonicWaves()
    {
        return 0f;
    }
}
