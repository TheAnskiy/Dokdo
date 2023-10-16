using UnityEngine;


public class OceanShaderParameters : MonoBehaviour
{
    [SerializeField] private Material _oceanMaterial;

    [Header("Настройки математической модели воды:")]
    public Vector2[] DirectionWave;
    public float[] StepnessWave;
    public float[] LenghtWave;
    public float[] SpeedWave;

    void FixedUpdate()
    {
        // Волны герстнера:
        DirectionWave[0] = _oceanMaterial.GetVector("_Direction_Wave_1");
        StepnessWave[0] = _oceanMaterial.GetFloat("_Stepness_Wave_1");
        LenghtWave[0] = _oceanMaterial.GetFloat("_Lenght_Wave_1");
        SpeedWave[0] = _oceanMaterial.GetFloat("_Speed_Wave_1");

        DirectionWave[1] = _oceanMaterial.GetVector("_Direction_Wave_2");
        StepnessWave[1] = _oceanMaterial.GetFloat("_Stepness_Wave_2");
        LenghtWave[1] = _oceanMaterial.GetFloat("_Lenght_Wave_2");
        SpeedWave[1] = _oceanMaterial.GetFloat("_Speed_Wave_2");

        DirectionWave[2] = _oceanMaterial.GetVector("_Direction_Wave_3");
        StepnessWave[2] = _oceanMaterial.GetFloat("_Stepness_Wave_3");
        LenghtWave[2] = _oceanMaterial.GetFloat("_Lenght_Wave_3");
        SpeedWave[2] = _oceanMaterial.GetFloat("_Speed_Wave_3");

        DirectionWave[3] = _oceanMaterial.GetVector("_Direction_Wave_4");
        StepnessWave[3] = _oceanMaterial.GetFloat("_Stepness_Wave_4");
        LenghtWave[3] = _oceanMaterial.GetFloat("_Lenght_Wave_4");
        SpeedWave[3] = _oceanMaterial.GetFloat("_Speed_Wave_4");
    }
}
