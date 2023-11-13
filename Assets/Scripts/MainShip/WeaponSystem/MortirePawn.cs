using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class MortirePawn : MonoBehaviour
{
    [Header("Weapon shooting settings:")]
    [SerializeField] private GameObject _projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MortireShoot();
    }

    void MortireShoot()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 objectHit = hit.point;
                if (Input.GetKeyDown(KeyCode.Mouse2))
                    CreateProjectile(_projectile, objectHit);
            }
        }
    }

    public void CreateProjectile(GameObject projectile, Vector3 position)
    {
        Instantiate(projectile, position + new Vector3(0, 100, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        //Instantiate(muzzle_fx, Socket.position, Quaternion.identity * Socket.rotation);
    }
}
