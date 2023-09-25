using UnityEngine;


public class OceanShaderParameters : MonoBehaviour
{
    [SerializeField] private Material _oceanMaterial;

    [Header("Настройки математической модели воды:")]
    public Vector2 DirectionBasedWave;
    public float StepnessBasedWave;
    public float LenghtBasedWave;
    public float SpeedBasedWave;
    [Header("")]
    public Vector2 DirectionMiddleWave;
    public float StepnessMiddleWave;
    public float LenghtMiddleWave;
    public float SpeedMiddleWave;
    [Header("")]
    public Vector2 DirectionMicroWave;
    public float StepnessMicroWave;
    public float LenghtMicroWave;
    public float SpeedMicroWave;
    [Header("")]
    public float HeightWaves;
    public float TilingWaves;
    public float SpeedWaves;

    void FixedUpdate()
    {
        // Волны герстнера:
        DirectionBasedWave = _oceanMaterial.GetVector("_Direction_Based_Wave");
        StepnessBasedWave = _oceanMaterial.GetFloat("_Stepness_Based_Wave");
        LenghtBasedWave = _oceanMaterial.GetFloat("_Lenght_Based_Wave");
        SpeedBasedWave = _oceanMaterial.GetFloat("_Speed_Based_Wave");

        DirectionMiddleWave = _oceanMaterial.GetVector("_Direction_Middle_Wave");
        StepnessMiddleWave = _oceanMaterial.GetFloat("_Stepness_Middle_Wave");
        LenghtMiddleWave = _oceanMaterial.GetFloat("_Lenght_Middle_Wave");
        SpeedMiddleWave = _oceanMaterial.GetFloat("_Speed_Middle_Wave");

        DirectionMicroWave = _oceanMaterial.GetVector("_Direction_Micro_Wave");
        StepnessMicroWave = _oceanMaterial.GetFloat("_Stepness_Micro_Wave");
        LenghtMicroWave = _oceanMaterial.GetFloat("_Lenght_Micro_Wave");
        SpeedMicroWave = _oceanMaterial.GetFloat("_Speed_Micro_Wave");

        // Дополнительные шумовые волны:
        HeightWaves = _oceanMaterial.GetFloat("_HeightWaves");
        TilingWaves = _oceanMaterial.GetFloat("_TilingWaves");
        SpeedWaves = _oceanMaterial.GetFloat("_SpeedWaves");
    }
}
