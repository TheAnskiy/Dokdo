using System.Collections;
using UnityEngine;

public class FishingZone : MonoBehaviour
{
    [Header("��������� ������ ����:")]
    [SerializeField] private int _fishCount = 100;
    [SerializeField] private float _timeReload = 15;

    [Header("�������� ������ ����:")]
    [SerializeField] private uint _goldValue = 0;
    [SerializeField] private uint _fishValue = 1;

    private int _currFishCount;
    private bool _inside = false;
    private bool _onReload = false;
    public GameObject _child;

    private void Start()
    {
        _currFishCount = _fishCount;
    }

    private void Update()
    {
        if (_currFishCount <= 0)
        {
            if (!_onReload)
            {
                StartCoroutine(Reload());
                _child.SetActive(false);
                Debug.Log("��������");
                _onReload = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _inside = true;
            StartCoroutine(AccrualResources());
            Debug.Log("�� �o��� � ���� ������ ����");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _inside = false;
            StopCoroutine(AccrualResources());
            Debug.Log("�� ����� �� ���� ������ ����");
        }
    }

    private IEnumerator AccrualResources()
    {
        while (_inside)
        {
            if (_currFishCount > 0)
            {
                Debug.Log("��������� �������");
                StructCurrency.Gold += _goldValue;
                StructCurrency.Fish += _fishValue;
                _currFishCount -= (int)_fishValue;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_timeReload);
        Debug.Log("�����������");
        _child.SetActive(true);
        _currFishCount = _fishCount;
        _onReload = false;
    }
}
