using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Cannon))]
public class ExampleEditor : Editor
{
    float dist = 200;
    float horAngle = 30;
    float minVertAngle = -15;
    float maxVertAngle = 30;
    float thickness = 1;

    public void OnSceneGUI()
    {
        Cannon t = target as Cannon;
        Transform tr = t.transform;
        Handles.color = Color.yellow;

        var data = t.GetDataForDrawFireRange();
        dist = data.dist;
        horAngle = data.horAngle;
        minVertAngle = data.minVertAngl;
        maxVertAngle = data.maxVertAngle;

        Vector3 upLeftDir = tr.forward.RotateAroundAxis(tr.up, -horAngle).RotateAroundAxis(tr.right.RotateAroundAxis(tr.up, -horAngle), -maxVertAngle);
        Vector3 downLeftDir = tr.forward.RotateAroundAxis(tr.up, -horAngle).RotateAroundAxis(tr.right.RotateAroundAxis(tr.up, -horAngle), -minVertAngle);
        Vector3 upRightDir = tr.forward.RotateAroundAxis(tr.up, horAngle).RotateAroundAxis(tr.right.RotateAroundAxis(tr.up, horAngle), -maxVertAngle);
        Vector3 downRightDir = tr.forward.RotateAroundAxis(tr.up, horAngle).RotateAroundAxis(tr.right.RotateAroundAxis(tr.up, horAngle), -minVertAngle);

        Handles.DrawLine(tr.position, tr.position + upLeftDir * dist, thickness);
        Handles.DrawLine(tr.position, tr.position + upRightDir * dist, thickness);
        Handles.DrawLine(tr.position, tr.position + downLeftDir * dist, thickness);
        Handles.DrawLine(tr.position, tr.position + downRightDir * dist, thickness);

        Handles.DrawWireArc(tr.position, tr.up.RotateAroundAxis(tr.right, -maxVertAngle), upLeftDir, Vector3.Angle(upLeftDir, upRightDir), dist, thickness);
        Handles.DrawWireArc(tr.position, tr.up.RotateAroundAxis(tr.right, -minVertAngle), downLeftDir, Vector3.Angle(downLeftDir, downRightDir), dist, thickness);
        Handles.DrawWireArc(tr.position, tr.right.RotateAroundAxis(tr.up, -horAngle), upLeftDir, Vector3.Angle(upLeftDir, downLeftDir), dist, thickness);
        Handles.DrawWireArc(tr.position, tr.right.RotateAroundAxis(tr.up, horAngle), upRightDir, Vector3.Angle(upRightDir, downRightDir), dist, thickness);
    }
}