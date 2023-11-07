using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PID))]
public class PIDDrawer : PropertyDrawer
{
    GUIContent[] subLabels = { new GUIContent("P"), new GUIContent("I"), new GUIContent("D") };
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, label);
        EditorGUI.MultiPropertyField(position, subLabels, property.FindPropertyRelative("_kP"));
        
        EditorGUI.EndProperty();
    }
}
