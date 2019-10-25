using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChunkEditorWindow : EditorWindow
{
    Vector2 virtualPosition;
    ChunkSetUp currentChunk;
    Editor currentChunkInspector;

    Texture separator;

    ChunkSetUp[] chunks = new ChunkSetUp[0];


    public static void OpenThisWindow()
    {
        ChunkEditorWindow myChunkEditorWindow = EditorWindow.GetWindow(typeof(ChunkEditorWindow)) as ChunkEditorWindow;

        myChunkEditorWindow.Init(Selection.activeObject as ChunkSetUp);
    }

    public void Init(ChunkSetUp assetTarget)
    {
        minSize = new Vector2(500, 100);
        virtualPosition = Vector2.zero;

        currentChunk = assetTarget;

        Selection.selectionChanged += OnSelectionChange;

        separator = EditorGUIUtility.Load("Assets/Materials/Line1.png") as Texture;

        Undo.undoRedoPerformed += MyUndoCallBack;

        Show();
    }

    void MyUndoCallBack()
    {
        ChunkEditorWindow[] cew = Resources.FindObjectsOfTypeAll(typeof(ChunkEditorWindow)) as ChunkEditorWindow[];
        if (cew.Length != 0)
        {

            for (int i = 0; i < cew.Length; i++)
            {
                cew[i].Repaint();
            }
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    private void OnSelectionChange()
    {
        if (Selection.activeObject is ChunkSetUp)
        {
            currentChunk = Selection.activeObject as ChunkSetUp;
            Repaint();
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal("box");

        GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 500);
        var centeredLabel = GUI.skin.GetStyle("Label");
        centeredLabel.alignment = TextAnchor.MiddleCenter;


        

        float spaceX = 20;
        float lineByTwo = EditorGUIUtility.singleLineHeight * 2;

        float inspectorWidth = Screen.width;

        float[] xPosition = new float[currentChunk.objectId.Length];
        for (int i = 0; i < currentChunk.objectId.Length; i++)
        {
            float dividedInspector = (inspectorWidth - (spaceX * 2)) / currentChunk.objectId.Length;

            xPosition[i] = (dividedInspector + 0) + dividedInspector * i;
        }



        GUI.Label(new Rect(Screen.width / 2 - 50, 10, 100, 50), "Configuration", EditorStyles.boldLabel);

        float[] separatorPosition = new float[3];
        separatorPosition[0] = (xPosition[7] + (xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7]) / 2;
        separatorPosition[1] = (xPosition[13] + (xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7]) / 2;
        separatorPosition[2] = (xPosition[19] + (xPosition[6] - xPosition[7])) + (xPosition[6] + 20 - xPosition[7]) / 2;

        GUI.Label(new Rect(xPosition[7] + 20, lineByTwo * 2.5f, 200, 50), "Face du niveau visible", EditorStyles.boldLabel);


        //Debug.Log(separatorPosition[0]);

        for (int i = 0; i < 3; i++)
        {
            GUI.DrawTexture(new Rect(separatorPosition[i], lineByTwo + 14, 2, 20), separator);
        }

        for (int i = 0; i < currentChunk.objectId.Length; i++)
        {
            float y = lineByTwo + EditorGUIUtility.singleLineHeight;
            string buttonName = " ";

            Vector2 position = new Vector2(xPosition[i], y);
            Vector2 size = new Vector2(20, 20);

            if ((int)currentChunk.objectId[i] == 0)
            {
                GUI.color = Color.white;
                buttonName = " ";
            }

            if ((int)currentChunk.objectId[i] == 2)
            {
                GUI.color = Color.red;
                buttonName = " E";
            }

            if ((int)currentChunk.objectId[i] == 1)
            {
                GUI.color = Color.black;
                buttonName = " W";
            }

            if ((int)currentChunk.objectId[i] == 3)
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

        Undo.RecordObject(currentChunk, "Edited Something");

        ChunkSetUp.ObjectIDs currentEnum = currentChunk.objectId[enumIndex]++;

        if ((int)currentChunk.objectId[enumIndex] > 3)
        {
            currentChunk.objectId[enumIndex] = ChunkSetUp.ObjectIDs.Nothing;
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());


        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < chunks.Length; i++)
            {
                chunks[i].objectId[i] = currentChunk.objectId[enumIndex];
            }

            //On sauvegarde
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
