using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private RectTransform _healthSlider;

    private Camera _camera;
    private RectTransform _fillArea;
    private TestAI _enemy;
    private float maxValue;

    private void Awake()
    {
        _fillArea = _healthSlider.GetChild(1) as RectTransform;

        maxValue = _healthSlider.sizeDelta.x;
        _enemy = GetComponentInParent<TestAI>();

        _camera = Camera.main; // Надо сделать другой доступ к камере
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform);
    }

    private void UpdateHealthSlider(float maxHealth, float currentHealth)
    {
        _fillArea.sizeDelta = new Vector2(currentHealth / maxHealth * maxValue, _fillArea.sizeDelta.y);
    }

    private void OnEnable()
    {
        _enemy.OnHealthChanged += UpdateHealthSlider;
    }

    private void OnDisable()
    {
        _enemy.OnHealthChanged -= UpdateHealthSlider;
    }
}
