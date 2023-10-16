using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Laba2Recht : MonoBehaviour
{
    public bool RefreshStart;

    [Header("(Вводим) Факторные значения стартовые:")]
    [SerializeField] private double X1_0;
    [SerializeField] private double n1;
    [Space(15)]
    [SerializeField] private double X2_0;
    [SerializeField] private double n2;
    [Space(15)]
    [SerializeField] private double X3_0;
    [SerializeField] private double n3;
    [Space(15)]
    [SerializeField] private double C;

    [Header("(Вводим) Стартовые значения y:")]
    [SerializeField] private double[] y_nach;

    [Header("(Считываем) Стартовые значения b:")]
    [SerializeField] private double b0;
    [SerializeField] private double b1;
    [SerializeField] private double b2;
    [SerializeField] private double b3;
    [SerializeField] private double b11;
    [SerializeField] private double b22;
    [SerializeField] private double b33;
    [SerializeField] private double b12;
    [SerializeField] private double b13;
    [SerializeField] private double b23;
    [SerializeField] private double b123;

    [Header("(Считываем) Иксы:")]
    [SerializeField] private double X01;
    [SerializeField] private double X02;
    [SerializeField] private double X03;

    [Header("(Считываем) Коэффициенты B:")]
    [SerializeField] private double B0;
    [SerializeField] private double B1;
    [SerializeField] private double B2;
    [SerializeField] private double B3;
    [SerializeField] private double B12;
    [SerializeField] private double B13;
    [SerializeField] private double B23;
    [SerializeField] private double B123;
    [SerializeField] private double B11;
    [SerializeField] private double B22;
    [SerializeField] private double B33;
    [Space(25)]
    public bool RefreshEnd;
    [Header("(Вводим) Иксы:")]
    [SerializeField] private double X1;
    [SerializeField] private double X2;
    [SerializeField] private double X3;

    [Header("(Вводим) Истинное значение Y:")]
    [SerializeField] private double Y_MM;

    [Header("(Считываем) Y:")]
    [SerializeField] private double Y;

    [Header("(Считываем) Разница истинного и полученного:")]
    [SerializeField] private double dY;

    private void Update()
    {
        if ((RefreshStart) || (RefreshEnd))
            Recht();
    }

    void Recht()
    {
        b0 = b1 = b2 = b3 = b11 = b22 = b33 = b12 = b13 = b23 = b123 = 0;
        //------------------------------------------------
        b0 = 0;
        // Рассчет коэффициентов b:
        for (int i = 0; i < 15; i++)
            b0 += y_nach[i];
        b0 /= 15;
        //------------------------------------------------
        int sign = 0;
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
                sign = -1;
            else
                sign = +1;
            b1 += y_nach[i] * sign;
        }
        b1 -= y_nach[8] * 1.215f;
        b1 += y_nach[9] * 1.215f;
        b1 /= 10.95245f;
        //------------------------------------------------
        int sign_1 = 1;
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
                sign_1 *= -1;
            b2 += y_nach[i] * sign_1;
        }
        b2 -= y_nach[10] * 1.215f;
        b2 += y_nach[11] * 1.215f;
        b2 /= 10.95245f;
        //------------------------------------------------
        int sign_2 = 1;
        for (int i = 0; i < 8; i++)
        {
            if (i % 4 == 0)
                sign_2 *= -1;
            b3 += y_nach[i] * sign_2;
        }
        b3 -= y_nach[12] * 1.215f;
        b3 += y_nach[13] * 1.215f;
        b3 /= 10.95245f;
        //------------------------------------------------
        b12 = (y_nach[0] - y_nach[1] - y_nach[2] + y_nach[3] + y_nach[4] - y_nach[5] - y_nach[6] + y_nach[7]) / 8;
        b13 = (y_nach[0] - y_nach[1] + y_nach[2] - y_nach[3] - y_nach[4] + y_nach[5] - y_nach[6] + y_nach[7]) / 8;
        b23 = (y_nach[0] + y_nach[1] - y_nach[2] - y_nach[3] - y_nach[4] - y_nach[5] + y_nach[6] + y_nach[7]) / 8;
        b123 = (-y_nach[0] + y_nach[1] + y_nach[2] - y_nach[3] + y_nach[4] - y_nach[5] - y_nach[6] + y_nach[7]) / 8;
        //------------------------------------------------
        for (int i = 0; i < 8; i++)
            b11 += y_nach[i] * 0.27f;
        for (int i = 8; i < 10; i++)
            b11 += y_nach[i] * 0.75f;
        for (int i = 10; i < 15; i++)
            b11 += y_nach[i] * -0.73f;
        b11 /= 4.3727d;
        //------------------------------------------------
        for (int i = 0; i < 8; i++)
            b22 += y_nach[i] * 0.27f;
        for (int i = 8; i < 15; i++)
        {
            if (i == 10 || i == 11)
                b22 += y_nach[i] * 0.75f;
            else
                b22 += y_nach[i] * -0.73f;
        }
        b22 /= 4.3727d;
        //------------------------------------------------
        for (int i = 0; i < 8; i++)
            b33 += y_nach[i] * 0.27f;
        for (int i = 8; i < 15; i++)
        {
            if (i == 12 || i == 13)
                b33 += y_nach[i] * 0.75f;
            else
                b33 += y_nach[i] * -0.73f;
        }
        b33 /= 4.3727d;

        // Рассчет коэффициентов X:
        X01 = X1_0;
        X02 = X2_0;
        X03 = X3_0;

        // Рассчет коэффициентов B:
        B0 = b0 - (b1 * X1_0) / n1 - (b2 * X02) / n2 - (b3 * X03) / n3 +
            (b12 * X1_0 * X02) / (n1 * n2) + (b13 * X1_0 * X03) / (n1 * n3) + (b23 * X02 * X03) / (n2 * n3) - (b123 * X1_0 * X02 * X03) / (n1 * n2 * n3) -
            (b11 * C) - (b22 * C) - (b33 * C) + (b11 * X1_0 * X1_0) / (n1 * n1) + (b22 * X02 * X02) / (n2 * n2) + (b33 * X03 * X03) / (n3 * n3);

        B1 = b1 / n1 - (b12 * X02) / (n1 * n2) - (b13 * X03) / (n1 * n3) + (b123 * X02 * X03) / (n1 * n2 * n3) - 2 * (b11 * X1_0) / (n1 * n1);
        B2 = b2 / n2 - (b12 * X1_0) / (n1 * n2) - (b23 * X03) / (n2 * n3) + (b123 * X1_0 * X03) / (n1 * n2 * n3) - 2 * (b22 * X02) / (n2 * n2);
        B3 = b3 / n3 - (b13 * X1_0) / (n1 * n3) - (b23 * X02) / (n2 * n3) + (b123 * X1_0 * X02) / (n1 * n2 * n3) - 2 * (b33 * X03) / (n3 * n3);

        B12 = b12 / (n1 * n2) - (b123 * X03) / (n1 * n2 * n3);
        B13 = b13 / (n1 * n3) - (b123 * X02) / (n1 * n2 * n3);
        B23 = b23 / (n2 * n3) - (b123 * X1_0) / (n1 * n2 * n3);
        B123 = b123 / (n1 * n2 * n3);

        B11 = b11 / (n1 * n1);
        B22 = b22 / (n2 * n2);
        B33 = b33 / (n3 * n3);

        // Рассчет финального Y исходя из разных значений факторов:
        Y = B0 + (B1 * X1) + (B2 * X2) + (B3 * X3) +
            (B11 * (X1 * X1)) + (B22 * (X2 * X2)) + (B33 * (X3 * X3)) +
            (B12 * X1 * X2) + (B13 * X1 * X3) + (B23 * X2 * X3) + (B123 * X1 * X2 * X3);
        dY = Y - Y_MM;
        RefreshStart = RefreshEnd = false;
    }
}
