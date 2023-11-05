[System.Serializable]
public class PID
{
    [UnityEngine.SerializeField] private float _kP, _kI, _kD;

    public PID(float kP, float kI, float kD)
    {
        _kP = kP;
        _kI = kI;
        _kD = kD;
    }

    private float _p, _i, _d;
    private float _previouseError;

    public void SetParams(float kP, float kI, float kD)
    {
        _kP = kP;
        _kI = kI;
        _kD = kD;
    }

    public float GetOutput(float currentError, float deltaTime)
    {
        _p = currentError;
        _i += _p * deltaTime;
        _d = (_p - _previouseError) / deltaTime;
        _previouseError = currentError;

        return _p * _kP + _i * _kI + _d * _kD;
    }
}
