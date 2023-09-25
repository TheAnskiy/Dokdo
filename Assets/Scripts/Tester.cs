using UnityEngine;

[ExecuteInEditMode]
public class Tester : MonoBehaviour
{
    [Header("Коэффициенты B:")]
    [SerializeField] private float B0 = 0.0221325f;
    [SerializeField] private float B1 = 0.00253675f;
    [SerializeField] private float B2 = 0.002387f;
    [SerializeField] private float B3 = -5.925f;
    [SerializeField] private float B12 = -0.0000227f;
    [SerializeField] private float B13 = 1.5145f;
    [SerializeField] private float B23 = -0.03f;
    [SerializeField] private float B123 = -0.0078f;

    [Header("Иксы:")]
    [SerializeField] private float X1;
    [SerializeField] private float X2;
    [SerializeField] private float X3;

    [SerializeField] private float Y_MM;

    [Header("Ответ:")]
    [SerializeField] private float Y;
    [SerializeField] private float dY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Y = B0 + (B1 * X1) + (B2 * X2) + (B3 * X3) + (B12 * X1 * X2) + (B13 * X1 * X3) + (B23 * X2 * X3) + (B123 * X1 * X2 * X3);
        dY = Mathf.Abs(Y - Y_MM);
    }
}
