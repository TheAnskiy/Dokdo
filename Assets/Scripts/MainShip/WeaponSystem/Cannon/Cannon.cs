using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cannon : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _horizontal;
    [SerializeField] private Transform _vertical;

    [SerializeField] private DefaultCore _projectilePrefab;

    [SerializeField] private float _verticaRotateSpeed = 1.0f;
    [SerializeField] private float _horizontalRotateSpeed = 1.0f;

    [SerializeField] private float _verticalMaxAngle = 75.0f;
    [SerializeField] private float _verticalMinAngle = -15.0f;
    [SerializeField] private float _horizontalAngle = 30.0f;

    [SerializeField] private float _timeBetweenShots = 2.0f;

    [SerializeField] private float _firingRange = 100.0f;

    [SerializeField] private float _speedBullet = 30.0f;

    private Coroutine _coroutineAttack;
    private bool isFiring => _coroutineAttack != null;

    private void Update()
    { 

        Vector3 _toTarget = _target.position - transform.position;
        float distanceToTarget = _toTarget.magnitude;

        float rotX = NormalizeAngle(_vertical.localEulerAngles.x);
        float rotY = NormalizeAngle(_horizontal.localEulerAngles.y);

        bool isClampX = rotX > -_verticalMaxAngle && rotX < -_verticalMinAngle;
        bool isClampY = Mathf.Abs(rotY) < _horizontalAngle;

        if (distanceToTarget < _firingRange)
        {
            LookAtTarget();

            if (isFiring == false && isClampX && isClampY)
            {
                _coroutineAttack = StartCoroutine(Attack());
            }
            else if (isFiring == true && !(isClampX && isClampY))
            {
                StopCoroutine(_coroutineAttack);
                _coroutineAttack = null;
            }
        }
        else if (isFiring == true)
        {
            StopCoroutine(_coroutineAttack);
            _coroutineAttack = null;
        }
    }

    private IEnumerator Attack()
    {
        do
        {
            DefaultCore core = Instantiate(_projectilePrefab, _vertical.transform.position, _vertical.rotation);
            core.SetSpeed(_speedBullet);
            yield return new WaitForSeconds(_timeBetweenShots);
        } while (true);
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
            return;

        if (_target == newTarget)
            return;

        _target = newTarget;
    }

    private void OnDestroy()
    {
        if (_coroutineAttack != null)
            StopCoroutine(_coroutineAttack);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle < -180.0f)
        {
            angle += 360;
        }
        else if (angle > 180.0f)
        {
            angle -= 360.0f;
        }

        return angle;
    }

    private void LookAtTarget()
    {
        if (TryDeterminingAimingPoint(out Vector3 position) == false)
            return;

        Vector3 horizontalToTarget = position - _horizontal.position;
        Vector3 dirToTargetXZ = Vector3.ProjectOnPlane(horizontalToTarget, transform.up);
        float angleY = Vector3.SignedAngle(transform.forward, dirToTargetXZ, transform.up);
        float clampedAngleY = Mathf.Clamp(angleY, -_horizontalAngle, _horizontalAngle);
        _horizontal.localRotation = Quaternion.RotateTowards(_horizontal.localRotation, Quaternion.Euler(new Vector3(0, clampedAngleY, 0)), _horizontalRotateSpeed);

        Vector3 verticalToTarget = position - _vertical.position;
        Vector3 dirToTargetYZ = Vector3.ProjectOnPlane(verticalToTarget, _horizontal.right);
        float angleX = Vector3.SignedAngle(_horizontal.forward, dirToTargetYZ, _horizontal.right);
        float clampedAngleX = Mathf.Clamp(angleX, -_verticalMaxAngle, -_verticalMinAngle);
        _vertical.localRotation = Quaternion.RotateTowards(_vertical.localRotation, Quaternion.Euler(new Vector3(clampedAngleX, 0, 0)), _verticaRotateSpeed);
    }

    private bool TryDeterminingAimingPoint(out Vector3 aimPoint)
    {
        
        Vector3 dir = _target.position - transform.position;
        float x = new Vector3(dir.x, dir.z).magnitude; // Дистанция до цели
        float y = dir.y; // Возвышение цели

        // Коефициенты квадратного уравнения
        float a = Physics.gravity.y * x * x / (2 * _speedBullet * _speedBullet);
        float b = x;
        float c = a - y;

        (float x1, float x2) = SolveQuadratic(a, b, c);
        if (float.IsNaN(x1))
        {
            aimPoint = Vector3.zero;
            return false;
        }

        float angleAttak = Mathf.Rad2Deg * Mathf.Atan(x1);

        float projectileFlightTime = x / (_speedBullet * Mathf.Cos(angleAttak * Mathf.Deg2Rad));

        float loweringProjectile = (Physics.gravity.y * projectileFlightTime * projectileFlightTime) / 2;

        aimPoint = _target.position + new Vector3(0, -loweringProjectile, 0);
        return true;
    }

    private (float, float) SolveQuadratic(float a, float b, float c)
    {
        float D = b * b - 4 * a * c;

        if (D < 0)
            return (float.NaN, float.NaN);

        float x1 = (-b + Mathf.Sqrt(D)) / (2 * a);
        float x2 = (-b - Mathf.Sqrt(D)) / (2 * a);
        return (x1, x2);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        DrawFiringSector();
    }

    private void DrawFiringSector()
    {
        float thickness = 1.0f;

        Vector3 upLeftDir = transform.forward.RotateAroundAxis(transform.up, -_horizontalAngle).
            RotateAroundAxis(transform.right.RotateAroundAxis(transform.up, -_horizontalAngle), -_verticalMaxAngle);

        Vector3 downLeftDir = transform.forward.RotateAroundAxis(transform.up, -_horizontalAngle).
            RotateAroundAxis(transform.right.RotateAroundAxis(transform.up, -_horizontalAngle), -_verticalMinAngle);

        Vector3 upRightDir = transform.forward.RotateAroundAxis(transform.up, _horizontalAngle).
            RotateAroundAxis(transform.right.RotateAroundAxis(transform.up, _horizontalAngle), -_verticalMaxAngle);

        Vector3 downRightDir = transform.forward.RotateAroundAxis(transform.up, _horizontalAngle).
            RotateAroundAxis(transform.right.RotateAroundAxis(transform.up, _horizontalAngle), -_verticalMinAngle);

        Vector3 centerLeftDir = transform.forward.RotateAroundAxis(transform.up, -_horizontalAngle).
            RotateAroundAxis(transform.right.RotateAroundAxis(transform.up, -_horizontalAngle), -(_verticalMinAngle + (_verticalMaxAngle - _verticalMinAngle)/2));

        Handles.color = Color.yellow;
        Handles.DrawLine(transform.position, transform.position + upLeftDir * _firingRange, thickness);
        Handles.DrawLine(transform.position, transform.position + upRightDir * _firingRange, thickness);
        Handles.DrawLine(transform.position, transform.position + downLeftDir * _firingRange, thickness);
        Handles.DrawLine(transform.position, transform.position + downRightDir * _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.right,
            transform.forward.RotateAroundAxis(transform.right, -_verticalMaxAngle), 
            (_verticalMaxAngle - _verticalMinAngle), _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.up,
            centerLeftDir, _horizontalAngle * 2, _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.up,
            upLeftDir, _horizontalAngle * 2, _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.up,
            downLeftDir, _horizontalAngle * 2, _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.right.RotateAroundAxis(transform.up, -_horizontalAngle),
            upLeftDir, Vector3.Angle(upLeftDir, downLeftDir), _firingRange, thickness);

        Handles.DrawWireArc(transform.position, transform.right.RotateAroundAxis(transform.up, _horizontalAngle),
            upRightDir, Vector3.Angle(upRightDir, downRightDir), _firingRange, thickness);

        Handles.color = Color.red;
        Handles.DrawLine(_vertical.transform.position, _vertical.transform.position + _vertical.transform.forward * _firingRange, thickness);
    }

#endif
}
