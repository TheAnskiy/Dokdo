using System.Collections;
using UnityEngine;

public class MainShipParameters : MonoBehaviour, IDamageable
{
    public BuoyController buoyController;
    //[Header("Параметры живучести:")]
    [field: SerializeField] public float Health { get; private set; } = 100.0f;

    private bool _isInverted = false;
    private float _timerInverted = 0.0f;
    private float _lifeTimeInverted = 5.0f;
    private bool _isDead = false;

    // Start is called before the first frame updates
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KillInverted();
    }

    public void TakeDamage(GameObject damageCauser, float damageAmount)
    {
        if (Health> 0)
            Health -= damageAmount;

        if (Health <= 0)
            StartCoroutine(buoyController.Die(2f, 3f));
    }



    private void KillInverted()
    {
        if (transform.IsInverted())
        {
            if (_isInverted == false)
            {

                _isInverted = true;
            }

            _timerInverted -= Time.fixedDeltaTime;

            if (_timerInverted < -_lifeTimeInverted && _isDead == false)
            {
                StartCoroutine(Die());
            }
        }
        else if (_isInverted == true)
        {
            _isInverted = false;
            _timerInverted = 0.0f;
        }
    }
}
