using System.Collections;
using UnityEngine;

public class ProjectileFly : MonoBehaviour
{
    [SerializeField] public float _shotImpulse = 1;
    [SerializeField] private GameObject _fxHit;
    private GameObject Parent;
    private Rigidbody _rigidBody;

    void Awake()
    {
        Parent = GameObject.Find("Projectiles");
        _rigidBody = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        _rigidBody.AddForce(gameObject.transform.right * _shotImpulse, ForceMode.Impulse);
    }

    void Update()
    {
        StartCoroutine(Killer());
    }

    IEnumerator Killer()
    {
        gameObject.transform.SetParent(Parent.transform, true);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_fxHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
