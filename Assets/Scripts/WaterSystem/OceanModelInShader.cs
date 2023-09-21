using UnityEngine;


public class OceanModelInShader: MonoBehaviour
{
    [Header("Настройки математической модели воды:")]
    public Material _oceanMaterial;
    public float waveAmplitude = 0;
    public float waveFrequancy = 0;
    public float waveSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        if (_oceanMaterial.GetFloat("_AmplitudeWaves") != waveAmplitude)
            waveAmplitude = _oceanMaterial.GetFloat("_AmplitudeWaves");
        if (_oceanMaterial.GetFloat("_SpeedWaves") != waveSpeed)
            waveSpeed = _oceanMaterial.GetFloat("_SpeedWaves");
        if (_oceanMaterial.GetFloat("_FrequancyWaves") != waveFrequancy)
            waveFrequancy = _oceanMaterial.GetFloat("_FrequancyWaves");
    }
}
