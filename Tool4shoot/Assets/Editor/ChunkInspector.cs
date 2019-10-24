using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


[CustomEditor(typeof(ChunkSetUp))]
public class ChunkInspector : Editor
{

    ChunkSetUp targetChunk;
    ChunkSetUp[] chunks;
    Texture separator;


    private void OnEnable()
    {
        targetChunk = target as ChunkSetUp;
        chunks = new ChunkSetUp[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            chunks[i] = targets[i] as ChunkSetUp;
        }

        separator = EditorGUIUtility.Load("Assets/Materials/Line1.png") as Texture;
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        Undo.RecordObjects(chunks, "Edited Something");


        GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 1000);
        var centeredLabel = GUI.skin.GetStyle("Label");
        centeredLabel.alignment = TextAnchor.MiddleCenter;


        GUILayout.BeginHorizontal("box");

        float spaceX = 20;
        float lineByTwo = EditorGUIUtility.singleLineHeight * 2;

        float inspectorWidth = Screen.width;

        float[] xPosition = new float[targetChunk.objectId.Length];
        for (int i = 0; i < targetChunk.objectId.Length; i++)
        {
            float dividedInspector = (inspectorWidth - (spaceX * 2)) / targetChunk.objectId.Length;

            xPosition[i] = (dividedInspector + 0) + dividedInspector * i;
        }



        GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Configuration", EditorStyles.boldLabel);

        float[] separatorPosition = new float[3];
        separatorPosition[0] = (xPosition[7] +(xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7])/2;
        separatorPosition[1] = (xPosition[13] + (xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7]) / 2;
        separatorPosition[2] = (xPosition[19] + (xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7]) / 2;

        GUI.Label(new Rect(xPosition[8], lineByTwo * 2.5f, 100, 50), "Face\nvisible\nau début\n", EditorStyles.boldLabel);


        //Debug.Log(separatorPosition[0]);

        for (int i = 0; i < 3; i++)
        {
            GUI.DrawTexture(new Rect(separatorPosition[i], lineByTwo + 14, 2, 20), separator);
        }

        for (int i = 0; i < targetChunk.objectId.Length; i++)
        {
            float y = lineByTwo + EditorGUIUtility.singleLineHeight;
            string buttonName = " ";

            Vector2 position = new Vector2(xPosition[i], y);
            Vector2 size = new Vector2(20, 20);

            if ((int)targetChunk.objectId[i] == 0)
            {
                GUI.color = Color.white;
                buttonName = " ";
            }

            if ((int)targetChunk.objectId[i] == 2)
            {
                GUI.color = Color.red;
                buttonName = " E";
            }

            if ((int)targetChunk.objectId[i] == 1)
            {
                GUI.color = Color.black;
                buttonName = " W";
            }

            if ((int)targetChunk.objectId[i] == 3)
            {
                GUI.color = Color.yellow;
                buttonName = " C";
            }



            if (GUI.Button(new Rect(position, size), buttonName))
            {
                ButtonAssignation(i);
            }

        }

        GUILayout.EndHorizontal();

    }


    void ButtonAssignation(int enumIndex)
    {
        EditorGUI.BeginChangeCheck();

        Undo.RecordObjects(chunks, "Edited Something");

        ChunkSetUp.ObjectIDs currentEnum = targetChunk.objectId[enumIndex]++;

        if ((int)targetChunk.objectId[enumIndex] > 3)
        {
            targetChunk.objectId[enumIndex] = ChunkSetUp.ObjectIDs.Nothing;
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());


        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i].objectId[i] = targetChunk.objectId[enumIndex];
            }

            //On sauvegarde
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
