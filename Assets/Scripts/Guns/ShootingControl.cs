using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControl : MonoBehaviour
{
    public GameObject Projectile;
    private bool _reload = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (_reload))
            StartCoroutine(DoShoot());
    }

    void GunShooter()
    {
            Instantiate(Projectile, gameObject.transform.position, Quaternion.identity * gameObject.transform.rotation);
    }
    IEnumerator DoShoot()
    {
        GunShooter();
        _reload = false;
        yield return new WaitForSeconds(1f);
        _reload = true;
    }
}
