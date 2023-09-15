using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _firingReload;
    [SerializeField] private Transform[] _cannon;
    private bool _reload = true;

    // Start is called before the first frame update
    void Awake()
    {
        _cannon = GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (_reload))
            StartCoroutine(TakeShoot());
    }

    private void CreateProjectile(Transform cannonTransform)
    {
        Instantiate(_projectile, cannonTransform.position, Quaternion.identity * cannonTransform.rotation);
    }

    IEnumerator TakeShoot()
    {
        for (int i = 0; i < _cannon.Length; i++)
            CreateProjectile(_cannon[i]);
        _reload = false;
        yield return new WaitForSeconds(_firingReload);
        _reload = true;
    }
}
