using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ChunkSetUp))]
public class ChunkInspector : Editor
{

    ChunkSetUp targetChunk;



    private void OnEnable()
    {
        targetChunk = target as ChunkSetUp;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth, 1000);
        

        for (int i = 0; i < targetChunk.objectId.Length; i++)
        {
            if(GUI.Button(new Rect(10, 20, 10, 10), "LOL"))
            {
                Debug.Log("YES");
            }
        }
    }

    void DrawButton()
    {
        
    }
}
