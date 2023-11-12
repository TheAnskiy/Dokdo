using System.Collections;
using UnityEngine;

public class MainShipParameters : MonoBehaviour, IDamageable
{
    public BuoyController buoyController;
    //[Header("Параметры живучести:")]
    [field: SerializeField] public float Health { get; private set; } = 100.0f;

    // Start is called before the first frame updates
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(GameObject damageCauser, float damageAmount)
    {
        if (Health> 0)
            Health -= damageAmount;

        if (Health <= 0)
            StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        float oldForce = buoyController._floatingStrenge;
        float newForce = 0;
        float _time = 0f;
        while (_time < 1)
        {
            _time += 0.5f * Time.deltaTime;
            buoyController.SetFloatingParameters(Mathf.Lerp(oldForce, newForce, _time), buoyController._depth);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


}
