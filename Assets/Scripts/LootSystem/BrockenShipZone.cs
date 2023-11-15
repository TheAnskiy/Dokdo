using System.Collections;
using UnityEngine;

public class BrockenShipZone : MonoBehaviour
{
    private BuoyController buoyController;

    [Header("Ценность корабля:")]
    [SerializeField] private uint _goldValue;
    [SerializeField] private uint _silverValue;
    [SerializeField] private uint _coalValue;
    [SerializeField] private uint _ironValue;
    [SerializeField] private uint _woodValue;
    [SerializeField] private uint _fishValue;


    private void Awake()
    {
        buoyController = GetComponent<BuoyController>();
    }

    private void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(AccrualResources());
            Debug.Log("Вы вoшли в зону утонувшего корабля");
        }
    }

    private IEnumerator AccrualResources()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Добавлены ресурсы");
        StructCurrency.Gold += (uint)Random.Range(_goldValue / 2, _goldValue);
        StructCurrency.Silver += (uint)Random.Range(_silverValue / 2, _silverValue);
        StructCurrency.Coal += (uint)Random.Range(_coalValue / 2, _coalValue);
        StructCurrency.Iron += (uint)Random.Range(_ironValue / 2, _ironValue);
        StructCurrency.Wood += (uint)Random.Range(_woodValue / 2, _woodValue);
        StructCurrency.Fish += (uint)Random.Range(_fishValue / 2, _fishValue);

        StartCoroutine(buoyController.Die(2f, 3f));

    }
}
