using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPosition : MonoBehaviour
{
    [SerializeField] private GameObject _mainShip;
    [SerializeField] private Vector2 _offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(_mainShip.transform.position.x + _offset.x, transform.position.y, _mainShip.transform.position.z + _offset.y);
    }
}
