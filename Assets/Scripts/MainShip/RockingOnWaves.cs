using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingOnWaves : MonoBehaviour
{
    [SerializeField] private float FrequencyShake = 0;
    [SerializeField] private float AmplitudeShake = 0;
    private float _rotate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotate = Mathf.Sin(Time.time * FrequencyShake) * AmplitudeShake;
        gameObject.transform.eulerAngles = new Vector3(_rotate, gameObject.transform.eulerAngles.y, _rotate);
    }
}
