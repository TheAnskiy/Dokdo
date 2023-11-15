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
            StartCoroutine(buoyController.Die(2f, 3f));
    }




}
