using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Настройки слежения камеры за объектом:")]
    [SerializeField] private Transform _trackingObjectTransform;
    [SerializeField] private Vector3 _offsetFromTrackObject;
    [SerializeField] private float _trackingSpeed = 0;
    [SerializeField] private bool _useParentTransform = true;

    private void Awake()
    {
        if (_useParentTransform)
            _trackingObjectTransform = transform.parent;
    }

    private void LateUpdate()
    {
        FollowingAndTracking();
    }

    /// <summary>
    /// Скрипт слежения камеры за требуемым объектом
    /// </summary>
    void FollowingAndTracking()
    {
        Vector3 defaultCameraPosition = _trackingObjectTransform.position + _offsetFromTrackObject;
        Vector3 lagCameraPosition = Vector3.Lerp(transform.position, defaultCameraPosition, _trackingSpeed * Time.deltaTime);
        gameObject.transform.position = lagCameraPosition;

        gameObject.transform.LookAt(_trackingObjectTransform.position);
    }
}

