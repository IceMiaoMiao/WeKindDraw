using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.UI;
using UnityEngine.UI;

[CustomEditor(typeof(ShapeImage),true)]
public class ShapeImageEditor : ImageEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ShapeImage image = (ShapeImage) target;
        SerializedProperty sp = serializedObject.FindProperty("offset");
        EditorGUILayout.PropertyField(sp, new GUIContent("offset 偏移"));
        serializedObject.ApplyModifiedProperties();
        
    }
}
