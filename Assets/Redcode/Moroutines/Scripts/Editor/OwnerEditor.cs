using Redcode.Moroutines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Owner))]
public class OwnerEditor : Editor
{
    private SerializedProperty _moroutinesProperty;

    private GUISkin _skin;

    private GUIStyle _headerStyle;
    
    private GUIStyle _rowStyle;

    private void OnEnable()
    {
        _moroutinesProperty = serializedObject.FindProperty("_moroutines");

        _skin = Resources.Load<GUISkin>("Moroutines/Skin");
        _headerStyle = _skin.customStyles.FirstOrDefault(s => s.name == "header");
        _rowStyle = _skin.customStyles.FirstOrDefault(s => s.name == "row");

        EditorApplication.update += Update;
    }

    private void OnDisable() => EditorApplication.update -= Update;

    private void Update() => EditorUtility.SetDirty(serializedObject.targetObject);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((Owner)target), typeof(Owner), false);
        GUI.enabled = true;

        GUILayout.Label($"Moroutines: {_moroutinesProperty.arraySize}");
        
        EditorGUILayout.BeginVertical(_skin.box);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Index", _headerStyle);
        GUILayout.Label("Name", _headerStyle);
        GUILayout.Label("State", _headerStyle);
        GUILayout.Label("Last Result", _headerStyle);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _moroutinesProperty.arraySize; i++)
        {
            var moroutine = (Moroutine)_moroutinesProperty.GetArrayElementAtIndex(i).managedReferenceValue;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(i.ToString(), _rowStyle);
            GUILayout.Label(moroutine.Name, _rowStyle);
            GUILayout.Label(moroutine.CurrentState.ToString(), _rowStyle);
            GUILayout.Label(moroutine.LastResult?.ToString() ?? "null", _rowStyle);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();
    }

    
}
