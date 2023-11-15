using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [Header("Настройки подбора контейнера:")]
    private Transform _mainShipTransform;
    [SerializeField] private float _findDistance;
    [SerializeField] private float _pickUpSpeed = 1f;
    [SerializeField] private float _killDistance = 1f;
    [SerializeField] private bool _showBoundBox = false;

    private float _distanceBetween;
    private GameObject _mainShip;
    private bool _go = false;

    [Header("Ценность контейнера:")]
    [SerializeField] private uint _goldValue;
    [SerializeField] private uint _silverValue;
    [SerializeField] private uint _coalValue;
    [SerializeField] private uint _ironValue;
    [SerializeField] private uint _woodValue;
    [SerializeField] private uint _fishValue;

    // Start is called before the first frame update
    private void Awake()
    {
        _mainShip = GameObject.Find("MainShip");
        _mainShipTransform = _mainShip.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainShipTransform != null)
        {
            _distanceBetween = Vector3.Distance(_mainShipTransform.position, gameObject.transform.position);
            if (_distanceBetween < _findDistance)
                _go = true;
            if (_go)
                PickUp();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_showBoundBox)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _findDistance);
        }
    }

    /// <summary>
    /// Функция подбора лута
    /// </summary>
    private void PickUp()
    {
        Vector3 lagLootPosition = Vector3.Lerp(transform.position, _mainShipTransform.position, (_pickUpSpeed * Time.deltaTime) / _distanceBetween);
        gameObject.transform.position = lagLootPosition;
        if (_distanceBetween < _killDistance)
        {
            AccrualResources();
            Destroy(gameObject);
        }
    }

    public void AccrualResources()
    {
        StructCurrency.Gold += (uint)Random.Range(_goldValue / 2, _goldValue);
        StructCurrency.Silver += (uint)Random.Range(_silverValue / 2, _silverValue);
        StructCurrency.Coal += (uint)Random.Range(_coalValue / 2, _coalValue);
        StructCurrency.Iron += (uint)Random.Range(_ironValue / 2, _ironValue);
        StructCurrency.Wood += (uint)Random.Range(_woodValue / 2, _woodValue);
        StructCurrency.Fish += (uint)Random.Range(_fishValue / 2, _fishValue);
    }
}
