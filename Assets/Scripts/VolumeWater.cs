using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class VolumeWater : MonoBehaviour
{

    private OceanShaderParameters _oceanParameters;
    [SerializeField] private Volume _volume;

    // Start is called before the first frame update
    void Start()
    {
        _oceanParameters = FindObjectOfType<OceanShaderParameters>();
        if (SceneView.currentDrawingSceneView.camera.transform.position.y <= getHeightAtPosition(SceneView.currentDrawingSceneView.camera.transform.position))
            _volume.enabled = true;
        else
            _volume.enabled = false;
    }

    //private void Update()
    //{
    //    if (SceneView.currentDrawingSceneView.camera.transform.position.y <= getHeightAtPosition(SceneView.currentDrawingSceneView.camera.transform.position))
    //        _volume.enabled = true;
    //    else
    //        _volume.enabled = false;
    //}

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
