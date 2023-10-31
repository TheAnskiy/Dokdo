using UnityEngine;


public class OceanShaderParameters : MonoBehaviour
{
    public static OceanShaderParameters Instance { get; private set; }

    public Material _oceanMaterial;

    [Header("Настройки математической модели воды:")]
    public float[] DirectionWave;
    public float[] StepnessWave;
    public float[] LenghtWave;
    public float[] SpeedWave;

    private void Awake()
    {
        _oceanMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        // Волны герстнера:
        DirectionWave[0] = _oceanMaterial.GetFloat("_Direction_Wave_1");
        StepnessWave[0] = _oceanMaterial.GetFloat("_Stepness_Wave_1");
        LenghtWave[0] = _oceanMaterial.GetFloat("_Lenght_Wave_1");
        SpeedWave[0] = _oceanMaterial.GetFloat("_Speed_Wave_1");

        DirectionWave[1] = _oceanMaterial.GetFloat("_Direction_Wave_2");
        StepnessWave[1] = _oceanMaterial.GetFloat("_Stepness_Wave_2");
        LenghtWave[1] = _oceanMaterial.GetFloat("_Lenght_Wave_2");
        SpeedWave[1] = _oceanMaterial.GetFloat("_Speed_Wave_2");

        DirectionWave[2] = _oceanMaterial.GetFloat("_Direction_Wave_3");
        StepnessWave[2] = _oceanMaterial.GetFloat("_Stepness_Wave_3");
        LenghtWave[2] = _oceanMaterial.GetFloat("_Lenght_Wave_3");
        SpeedWave[2] = _oceanMaterial.GetFloat("_Speed_Wave_3");

        DirectionWave[3] = _oceanMaterial.GetFloat("_Direction_Wave_4");
        StepnessWave[3] = _oceanMaterial.GetFloat("_Stepness_Wave_4");
        LenghtWave[3] = _oceanMaterial.GetFloat("_Lenght_Wave_4");
        SpeedWave[3] = _oceanMaterial.GetFloat("_Speed_Wave_4");
    }

    public float GetHeightAtPosition(Vector3 position)
    {
        float time = Time.time;
        Vector3 currentPosition = GetAdditionWaveHeight(position, time);
        Vector3 diff = new Vector3(position.x - currentPosition.x, 0, position.z - currentPosition.z);
        currentPosition = GetAdditionWaveHeight(diff, time);
        return currentPosition.y;
    }

    private Vector3 GetAdditionWaveHeight(Vector3 position, float timeSinceStart)
    {
        Vector3 result = new Vector3();
        result = GerstnerWaveGenerator(position, DirectionWave[0], StepnessWave[0], LenghtWave[0], SpeedWave[0], timeSinceStart) +
            GerstnerWaveGenerator(position, DirectionWave[1], StepnessWave[1], LenghtWave[1], SpeedWave[1], timeSinceStart) +
            GerstnerWaveGenerator(position, DirectionWave[2], StepnessWave[2], LenghtWave[2], SpeedWave[2], timeSinceStart) +
            GerstnerWaveGenerator(position, DirectionWave[3], StepnessWave[3], LenghtWave[3], SpeedWave[3], timeSinceStart);
        return result;
    }

    /// <summary>
    /// Generator Gerstner waves
    /// </summary>
    /// <returns>Position Vertex in current place</returns>
    private Vector3 GerstnerWaveGenerator(Vector3 position, float angle, float steepness, float wavelength, float speed, float timeSinceStart)
    {
        float k = 2 * Mathf.PI / wavelength;

        Vector3 basedDirection = new Vector3(1, 1, 0);
        Vector3 normilizedDirection = basedDirection.RotateAroundAxis(Vector3.forward, angle);

        float f = k * Vector2.Dot(normilizedDirection, new Vector2(position.x, position.z)) - (speed * timeSinceStart);
        float a = steepness / k;

        return new Vector3(normilizedDirection.x * a * Mathf.Cos(f), a * Mathf.Sin(f), normilizedDirection.y * a * Mathf.Cos(f));
    }
}
