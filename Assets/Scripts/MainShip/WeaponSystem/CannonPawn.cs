using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CannonPawn : MonoBehaviour
{
    [Header("Cannon Settings:")]
    [SerializeField] private Transform Socket;
    [SerializeField] private Vector2 HorizontalArea = new Vector2(-25, 25);
    [SerializeField] private Vector2 VerticalArea = new Vector2(-25, 25);
    public bool IsLeftSide = false;

    private Vector3 defaultRotation;
    private Vector3 currentRotation;

    // Update is called once per first frame
    void Start()
    {
        Normilize(ref defaultRotation);
    }

    // Update is called once per frame
    void Update()
    {
        Normilize(ref currentRotation);
    }

    public void RotateHorizontal(float speed, float direction)
    {

        if ((direction < 0) && (currentRotation.y > defaultRotation.y + HorizontalArea.x))
            transform.localEulerAngles += new Vector3(0, speed * direction, 0);
        if ((direction > 0) && (currentRotation.y < defaultRotation.y + HorizontalArea.y))
            transform.localEulerAngles += new Vector3(0, speed * direction, 0);
    }

    public void RotateVectical(float speed, float direction)
    {
        if ((direction > 0) && (currentRotation.x < defaultRotation.x + VerticalArea.y))
            transform.localEulerAngles += new Vector3(speed * direction, 0, 0);

        if ((direction < 0) && (currentRotation.x > defaultRotation.x + VerticalArea.x))
            transform.localEulerAngles += new Vector3(speed * direction, 0, 0);
    }

    void Normilize(ref Vector3 rotation)
    {
        rotation = gameObject.transform.localEulerAngles;
        if (gameObject.transform.localEulerAngles.y > 180)
            rotation.y = gameObject.transform.localEulerAngles.y - 360f;

        if (gameObject.transform.localEulerAngles.x > 180)
            rotation.x = gameObject.transform.localEulerAngles.x - 360f;
    }

    public void CreateProjectile(GameObject projectile, GameObject muzzle_fx)
    {
        Instantiate(projectile, Socket.position, Quaternion.identity * Socket.rotation);
        Instantiate(muzzle_fx, Socket.position, Quaternion.identity * Socket.rotation);
    }
}
