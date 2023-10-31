using UnityEngine;

public class BuoySingle : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _strengeFloating = 250;
    [SerializeField] private float _depthBeforeSubmerged = 1f;

    private OceanShaderParameters _oceanParameters;

    // Отладочные параметры:
    private float _countBuoy = 1f;

    private float _waveHeight;

    void Awake()
    {
        _oceanParameters = FindObjectOfType<OceanShaderParameters>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Init gravity to buoy
        _rigidBody.AddForceAtPosition((Physics.gravity / _countBuoy) * Time.deltaTime, transform.position, ForceMode.Acceleration);

        _waveHeight = OceanShaderParameters.Instance.GetHeightAtPosition(transform.position);
        if (transform.position.y < _waveHeight)
            Floating();
    }

    /// <summary>
    /// The function applies an ejection force to the object, like water
    /// </summary>
    void Floating()
    {
        float displacementMultiplier = Mathf.Clamp01((_waveHeight - transform.position.y) / _depthBeforeSubmerged) * _strengeFloating * Time.deltaTime;
        _rigidBody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

        //_rigidBody.AddForce(/*displacementMultiplier * */-_rigidBody.velocity * Time.deltaTime, ForceMode.VelocityChange);
        //_rigidBody.AddTorque(/*displacementMultiplier * */-_rigidBody.angularVelocity * Time.deltaTime, ForceMode.VelocityChange);
    }
}
