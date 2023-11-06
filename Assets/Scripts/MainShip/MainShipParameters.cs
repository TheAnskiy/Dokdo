using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipParameters : MonoBehaviour
{
    [Header("Параметры живучести:")]
    [SerializeField] private int _Health = 100;
    private bool _inside = false;

    [Header("Ценность рыбной зоны:")]
    [SerializeField] private uint _goldValue = 0;
    [SerializeField] private uint _fishValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
