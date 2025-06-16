using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlateValue))]
public class PlateValueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // plateType 필드만 먼저 노출
        EditorGUILayout.PropertyField(serializedObject.FindProperty("plateType"));

        // 현재 plateType 값에 따라 다른 필드 노출
        PlateValue plate = (PlateValue)target;
        if (plate.plateType == PlateType.Number)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("numberValue"));
        }
        else if (plate.plateType == PlateType.Symbol)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("symbolValue"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
