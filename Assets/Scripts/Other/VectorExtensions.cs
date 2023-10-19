using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 RotateAroundAxis(this Vector3 vector, Vector3 axis, float angle)
    {
        // ��������� ���� � �������
        float radians = angle * Mathf.Deg2Rad;

        // ��������� ����� � ������� ���� ��������
        float sinAngle = Mathf.Sin(radians);
        float cosAngle = Mathf.Cos(radians);

        // ������� ������, ������������ ���
        Vector3 parallel = axis * Vector3.Dot(axis, vector);

        // ������� ������, ���������������� ���
        Vector3 perpendicular = vector - parallel;

        // ������������ ���������������� ������ ��������� ������������������ �������
        Vector3 rotatedPerpendicular = perpendicular * cosAngle + Vector3.Cross(axis, perpendicular) * sinAngle;

        // ���������� �������, ����� �������� �������� ���������
        return parallel + rotatedPerpendicular;
    }
}