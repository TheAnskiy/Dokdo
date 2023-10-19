using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 RotateAroundAxis(this Vector3 vector, Vector3 axis, float angle)
    {
        // Переводим угол в радианы
        float radians = angle * Mathf.Deg2Rad;

        // Вычисляем синус и косинус угла поворота
        float sinAngle = Mathf.Sin(radians);
        float cosAngle = Mathf.Cos(radians);

        // Находим вектор, параллельный оси
        Vector3 parallel = axis * Vector3.Dot(axis, vector);

        // Находим вектор, перпендикулярный оси
        Vector3 perpendicular = vector - parallel;

        // Поворачиваем перпендикулярный вектор используя тригонометрические функции
        Vector3 rotatedPerpendicular = perpendicular * cosAngle + Vector3.Cross(axis, perpendicular) * sinAngle;

        // Объединяем векторы, чтобы получить итоговый результат
        return parallel + rotatedPerpendicular;
    }
}