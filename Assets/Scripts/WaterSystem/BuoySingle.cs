using UnityEngine;

public class BuoySingle : MonoBehaviour
{
    [SerializeField] private float _strengeFloating = 250;
    [SerializeField] private float _depthBeforeSubmerged = 1f;

    private Rigidbody _rigidBody;
    private float _waveHeight;

    // Отладочные параметры:
    private float _countBuoy = 1f;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Add gravity
        _rigidBody.AddForceAtPosition((Physics.gravity / _countBuoy) * Time.deltaTime, transform.position, ForceMode.Acceleration);

        // Add ejection force
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

        // Adding the ejection compensation rate
        //_rigidBody.AddForce(/*displacementMultiplier * */-_rigidBody.velocity * Time.deltaTime, ForceMode.VelocityChange);
        //_rigidBody.AddTorque(/*displacementMultiplier * */-_rigidBody.angularVelocity * Time.deltaTime, ForceMode.VelocityChange);
    }
}
