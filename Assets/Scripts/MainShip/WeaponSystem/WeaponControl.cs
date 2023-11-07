using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public float speedRotate = 5f;
    public Vector2 HorizontalArea = new Vector2(-25, 25);
    public Vector2 VerticalArea = new Vector2(-25, 25);
    public bool Left = false;


    public Vector3 defaultRotation;
    public Vector3 currentRotation;

    private InputController _controller;

    // Start is called before the first frame update
    void Start()
    {
        Normilize(ref defaultRotation);
        _controller = GetComponentInParent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        Normilize(ref currentRotation);
        if (Left)
            RotateHorizontal(-_controller.inputMouse.x);
        else
            RotateHorizontal(_controller.inputMouse.x);
        RotateVectical(-_controller.inputMouse.y);
    }


    void RotateHorizontal(float direction)
    {
        if ((direction < 0) && (currentRotation.y > defaultRotation.y + HorizontalArea.x))
            gameObject.transform.localEulerAngles += new Vector3(0, speedRotate * direction, 0);
        if ((direction > 0) && (currentRotation.y < defaultRotation.y + HorizontalArea.y))
            gameObject.transform.localEulerAngles += new Vector3(0, speedRotate * direction, 0);
    }


    void RotateVectical(float direction)
    {
        if ((direction > 0) && (currentRotation.x < defaultRotation.x + VerticalArea.y))
            gameObject.transform.localEulerAngles += new Vector3(speedRotate * direction, 0, 0);

        if ((direction < 0) && (currentRotation.x > defaultRotation.x + VerticalArea.x))
            gameObject.transform.localEulerAngles += new Vector3(speedRotate * direction, 0, 0);
    }

    void Normilize(ref Vector3 rotation)
    {
        rotation = gameObject.transform.localEulerAngles;
        if (gameObject.transform.localEulerAngles.y > 180)
            rotation.y = gameObject.transform.localEulerAngles.y - 360f;

        if (gameObject.transform.localEulerAngles.x > 180)
            rotation.x = gameObject.transform.localEulerAngles.x - 360f;
    }
}
