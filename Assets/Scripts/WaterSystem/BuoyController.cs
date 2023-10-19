using UnityEngine;

public class BuoyController : MonoBehaviour
{
    [SerializeField] private bool _override = true;
    [SerializeField] private Buoy[] buoy;
    [SerializeField] private float _floatingStrenge;
    [SerializeField] private float _depth;

    // Start is called before the first frame update
    void Start()
    {
        buoy = GetComponentsInChildren<Buoy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_override)
        {
            SetFloatingStrenge(_floatingStrenge, _depth);
            _override = false;
        }
    }

    void SetFloatingStrenge(float floatingStrenge, float depth)
    {
        foreach (var buoy in buoy)
        {
            buoy._strengeFloating = floatingStrenge;
            buoy._depthBeforeSubmerged = depth;
        }
    }
}
