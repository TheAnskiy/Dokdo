using System.Collections;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private InputController _controller;
    [SerializeField] private CannonPawn[] _cannon;

    [Header("Weapon aiming settings:")]
    [SerializeField] private float _rotateSpeed = 5f;

    [Header("Weapon shooting settings:")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _fx;
    [SerializeField] private float _reloadingShooting;

    private bool _onReloadLeftBoard = false;
    private bool _onReloadRightBoard = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponentInParent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Control aiming weapon:
        if (_controller.controlWeaponRight)
        {
            for (int i = 0; i < _cannon.Length; i++)
            {
                if (!_cannon[i].IsLeftSide)
                {
                    _cannon[i].RotateHorizontal(_rotateSpeed, -_controller.inputMouse.x);
                    _cannon[i].RotateVectical(_rotateSpeed, -_controller.inputMouse.y);
                }
            }
        }
        if (_controller.controlWeaponLeft)
        {
            for (int i = 0; i < _cannon.Length; i++)
            {
                if (_cannon[i].IsLeftSide)
                {
                    _cannon[i].RotateHorizontal(_rotateSpeed, _controller.inputMouse.x);
                    _cannon[i].RotateVectical(_rotateSpeed, -_controller.inputMouse.y);
                }
            }
        }

        // Control shooting weapon:
        if (_controller.shotRight && !_onReloadRightBoard)
            StartCoroutine(TakeShoot(false));
        if (_controller.shotLeft && !_onReloadLeftBoard)
            StartCoroutine(TakeShoot(true));
    }

    IEnumerator TakeShoot(bool left)
    {
        for (int i = 0; i < _cannon.Length; i++)
            if (_cannon[i].IsLeftSide == left)
                _cannon[i].CreateProjectile(_projectile, _fx);
        if (!left)
            _onReloadRightBoard = true;
        else
            _onReloadLeftBoard = true;
        yield return new WaitForSeconds(_reloadingShooting);
        if (!left)
            _onReloadRightBoard = false;
        else
            _onReloadLeftBoard = false;
    }
}
