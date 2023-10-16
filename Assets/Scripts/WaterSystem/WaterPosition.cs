using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPosition : MonoBehaviour
{
    [SerializeField] private GameObject MainShip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(MainShip.transform.position.x, transform.position.y, MainShip.transform.position.z);
    }
}
