using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public float speedRotate = 5f;
    public float HorizontalArea = 5f;
    public float VerticalArea = 5f;
    public Vector3 defaultRotation;
    public Vector3 currentRotation;
    public bool Left = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = gameObject.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Normilize();

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    RotateHorizontal(-1);
        //if (Input.GetKey(KeyCode.RightArrow))
        //    RotateHorizontal(1);
        //if (Input.GetKey(KeyCode.UpArrow))
        //    RotateVectical(-1);
        //if (Input.GetKey(KeyCode.DownArrow))
        //    RotateVectical(1);

        if (Left)
        {
            if ((Input.GetAxis("Mouse X") > 0) && (Input.GetKey(KeyCode.Mouse2)))
                RotateHorizontal(1);
            if ((Input.GetAxis("Mouse X") < 0) && (Input.GetKey(KeyCode.Mouse2)))
                RotateHorizontal(-1);
        }
        else
        {
            if ((Input.GetAxis("Mouse X") > 0) && (Input.GetKey(KeyCode.Mouse2)))
                RotateHorizontal(-1);
            if ((Input.GetAxis("Mouse X") < 0) && (Input.GetKey(KeyCode.Mouse2)))
                RotateHorizontal(1);
        }

        if ((Input.GetAxis("Mouse Y") > 0) && (Input.GetKey(KeyCode.Mouse1)))
            RotateVectical(-1);
        if ((Input.GetAxis("Mouse Y") < 0) && (Input.GetKey(KeyCode.Mouse1)))
            RotateVectical(1);
    }

    void RotateHorizontal(int direction)
    {
        if ((direction < 0) && (currentRotation.y > -HorizontalArea))
            gameObject.transform.localEulerAngles += new Vector3(0, speedRotate * direction, 0);

        if ((direction > 0) && (currentRotation.y < HorizontalArea))
            gameObject.transform.localEulerAngles += new Vector3(0, speedRotate * direction, 0);
    }

    void RotateVectical(int direction)
    {
        if ((direction < 0) && (currentRotation.x > -VerticalArea))
            gameObject.transform.localEulerAngles += new Vector3(speedRotate * direction, 0, 0);

        if ((direction > 0) && (currentRotation.x < VerticalArea))
            gameObject.transform.localEulerAngles += new Vector3(speedRotate * direction, 0, 0);
    }

    void Normilize()
    {
        currentRotation = gameObject.transform.localEulerAngles;

        if (gameObject.transform.localEulerAngles.y > 180)
            currentRotation.y = gameObject.transform.localEulerAngles.y - 360f;

        if (gameObject.transform.localEulerAngles.x > 180)
            currentRotation.x = gameObject.transform.localEulerAngles.x - 360f;

        Debug.Log(currentRotation.x);
        //Debug.Log(currentRotation.y);
    }
}
