using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    [HideInInspector] public Vector3 inputMoveble;
    [HideInInspector] public Vector2 inputMouse;
    [HideInInspector] public bool controlWeaponRight;
    [HideInInspector] public bool controlWeaponLeft;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool shotRight, shotLeft;

    // Update is called once per frame
    void Update()
    {
        // Move
        inputMoveble = new Vector3(0, 0, 0);
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

        // Weapon control
        controlWeaponRight = Input.GetKey(KeyCode.Mouse1);
        controlWeaponLeft = Input.GetKey(KeyCode.Mouse0);
        inputMouse.x = Input.GetAxis("Mouse X");
        inputMouse.y = Input.GetAxis("Mouse Y");

        // Weapon use   
        shotLeft = false;
        shotRight = false;
        shotRight = Input.GetKeyUp(KeyCode.Mouse1);
        shotLeft = Input.GetKeyUp(KeyCode.Mouse0);
    }
}
