using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class PlayerInspector : Editor
{
    SerializedProperty smoothStrengthProperty;
    SerializedProperty playerSpeedProperty;

    private void OnEnable()
    {
        smoothStrengthProperty = serializedObject.FindProperty("smoothStrength");
        playerSpeedProperty = serializedObject.FindProperty("playerSpeed");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        serializedObject.Update();


        GUILayout.BeginVertical("box");


        EditorGUILayout.LabelField("Player Move Parameters");

        GUILayout.BeginHorizontal("box");


        EditorGUILayout.PropertyField(smoothStrengthProperty);
        EditorGUILayout.PropertyField(playerSpeedProperty);

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();


        serializedObject.ApplyModifiedProperties();

    }
}
