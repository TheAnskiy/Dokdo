using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Buoy : MonoBehaviour
{
    public Rigidbody _rigidBody;
    [SerializeField] OceanModelInShader _modelInShader;

    [Header("Физические настройки поведения обьекта над и под водой:")]
    [SerializeField] private float _underWaterDrag = 1f;
    [SerializeField] private float _underWaterAngularDrag = 12f;

    [SerializeField] private float _waveHeight;
    [SerializeField] private float _countBuoy = 1f;

    [SerializeField] private float _depthBeforeSubmerged = 1f;
    [SerializeField] private float _buoyVolume = 3;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _rigidBody.AddForceAtPosition(Physics.gravity * 2 / _countBuoy, transform.position, ForceMode.Acceleration);

        _waveHeight = WaveGenerator();
        if (transform.position.y < _waveHeight)
            Floating();
    }

    public float WaveGenerator()
    {
        // Гармоническая волна:
        Debug.Log(_modelInShader.waveFrequancy);
        return (_modelInShader.waveAmplitude * Mathf.Sin(transform.position.x * _modelInShader.waveFrequancy + _modelInShader.waveSpeed * Time.time)) + 
            (_modelInShader.waveAmplitude * Mathf.Sin(transform.position.z * _modelInShader.waveFrequancy + _modelInShader.waveSpeed * Time.time));
    }

    //public Vector3 GerstnerWaveGenerator(Vector3 position, Vector2 direction, float steepness, float wavelength, float speed, float timeSinceStart)
    //{
    //    float k = 2 * Mathf.PI / wavelength;

    //    Vector2 normilizedDirection = direction.normalized;

    //    float f = k * Vector2.Dot(normilizedDirection, new Vector2(position.x, position.z)) - (speed *timeSinceStart);
    //    float a = steepness / k;

    //    return new Vector3(normilizedDirection.x * a * Mathf.Cos(f), a * Mathf.Sin(f), normilizedDirection.y * a * Mathf.Cos(f));
    //}

    void Floating()
    {
        float displacementMultiplier = Mathf.Clamp01((_waveHeight - transform.position.y) / _depthBeforeSubmerged) * _buoyVolume;
        _rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), gameObject.transform.position, ForceMode.Acceleration);
       
        _rigidBody.AddForce(displacementMultiplier * -_rigidBody.velocity * _underWaterDrag * Time.deltaTime, ForceMode.VelocityChange);
        _rigidBody.AddTorque(displacementMultiplier * -_rigidBody.angularVelocity * _underWaterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
}
