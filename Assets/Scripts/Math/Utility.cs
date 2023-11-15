using UnityEngine;

public static class Utility
{
    public static bool IsInverted(this Transform transform)
    {
        return Vector3.Dot(Vector3.up, transform.up) < -0.5f;
    }
}
