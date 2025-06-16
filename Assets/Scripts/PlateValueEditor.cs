using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlateValue))]
public class PlateValueEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // plateType �ʵ常 ���� ����
        EditorGUILayout.PropertyField(serializedObject.FindProperty("plateType"));

        // ���� plateType ���� ���� �ٸ� �ʵ� ����
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
