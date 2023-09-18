using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _rigidBody.AddForceAtPosition(Physics.gravity / _countBuoy, transform.position, ForceMode.Acceleration);

        _waveHeight = WaveGenerator(gameObject.transform.position.x, gameObject.transform.position.y);
        if (transform.position.y < _waveHeight)
            Floating();
    }

    public float WaveGenerator(float x, float y)
    {
        // Двойная синуисоидальная волна:
        return _modelInShader.waveAmplitude * Mathf.Sin(x * _modelInShader.waveFrequancy - _modelInShader.waveSpeed * Time.time); /*+ _modelInShader.waveAmplitude * Mathf.Sin(y * _modelInShader.waveFrequancy - _modelInShader.waveSpeed * Time.time);*/
    }

    void Floating()
    {
        float displacementMultiplier = Mathf.Clamp01((_waveHeight - transform.position.y) / _depthBeforeSubmerged) * _buoyVolume;
        _rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), gameObject.transform.position, ForceMode.Acceleration);
       
        _rigidBody.AddForce(displacementMultiplier * -_rigidBody.velocity * _underWaterDrag * Time.deltaTime, ForceMode.VelocityChange);
        _rigidBody.AddTorque(displacementMultiplier * -_rigidBody.angularVelocity * _underWaterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
    }
}
