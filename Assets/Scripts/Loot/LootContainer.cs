using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LootContainer : MonoBehaviour
{
    [Header("Настройки подбора контейнера:")]
    [SerializeField] private Transform _mainShipTransform;
    [SerializeField] private float _findDistance;
    [SerializeField] private bool _showBoundBox = false;
    [SerializeField] private float _killDistance = 1f;
    private float _distanceBetween;
    private GameObject _mainShip;
    public float _pickUpSpeed = 1f;


    [Header("Ценность контейнера:")]
    [SerializeField] private uint _goldValue;
    [SerializeField] private uint _metallValue;
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
        _distanceBetween = Vector3.Distance(_mainShipTransform.position, gameObject.transform.position);
        if (_distanceBetween < _findDistance)
            PickUp();
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
        StructValues.Gold += (uint)Random.Range(_goldValue/2, _goldValue);
        StructValues.Metall += (uint)Random.Range(0, _metallValue);
        StructValues.Wood += (uint)Random.Range(0, _woodValue);
        StructValues.Fish += (uint)Random.Range(0, _fishValue);
    }
}
