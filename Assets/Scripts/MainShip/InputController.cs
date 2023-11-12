using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [HideInInspector] public Vector3 inputMoveble;
    [HideInInspector] public Vector2 inputMouse;
    [HideInInspector] public bool dash;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveble = new Vector3(0, 0, 0);
        // Move
        if (Input.GetKey(KeyCode.W))
            inputMoveble += transform.forward;
        if (Input.GetKey(KeyCode.S))
            inputMoveble += -transform.forward;
       // Rotate
        if (Input.GetKey(KeyCode.D))
            inputMoveble += transform.up;
        if (Input.GetKey(KeyCode.A))
            inputMoveble += -transform.up;
        // Dash
        dash = Input.GetKey(KeyCode.LeftShift);
        // Weapon
        //inputMouse = new Vector2(0,0);
        inputMouse.x = Input.GetAxis("Mouse X");
        inputMouse.y = Input.GetAxis("Mouse Y");
    }
}
