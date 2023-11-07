using UnityEngine;

public class Buoy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private bool _isReturn;

    [HideInInspector] public float _strengeFloating = 250;
    [HideInInspector] public float _depthBeforeSubmerged = 1.5f;

    private float _waveHeight;
    // Debug parameters:
    private float _countBuoy = 1f;

    void Awake()
    {
        if (_isReturn)
            _strengeFloating *= 2.5f;
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
        _rigidBody.AddForce(new Vector3(0, 0.25f * (-_rigidBody.velocity.y) * Time.deltaTime, 0), ForceMode.VelocityChange);
        //_rigidBody.AddTorque(/*displacementMultiplier * */-_rigidBody.angularVelocity * Time.deltaTime, ForceMode.VelocityChange);
    }
}
