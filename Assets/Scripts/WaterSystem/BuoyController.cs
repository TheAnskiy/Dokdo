using UnityEngine;

public class BuoyController : MonoBehaviour
{
    [SerializeField] private Buoy[] buoy;
    public float _floatingStrenge;
    private float _oldFloatingStrenge;
    public float _depth;

    // Start is called before the first frame update
    void Start()
    {
        buoy = GetComponentsInChildren<Buoy>();
        _oldFloatingStrenge = _floatingStrenge;
        SetFloatingParameters(_floatingStrenge, _depth);
    }

    // Update is called once per frame
    void Update()
    {
        if (buoy == null)
        {
            buoy = GetComponentsInChildren<Buoy>();
        }
        if (_floatingStrenge != _oldFloatingStrenge)
        {
            SetFloatingParameters(_floatingStrenge, _depth);
            //_floatingStrenge
        }
    }

    public void SetFloatingParameters(float floatingStrenge, float depth)
    {
        foreach (var buoy in buoy)
        {
            buoy._strengeFloating = floatingStrenge;
            buoy._depthBeforeSubmerged = depth;
        }
    }
}
