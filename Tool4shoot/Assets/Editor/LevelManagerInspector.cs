using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerInspector : Editor
{

    LevelManager levelManager;
    SerializedProperty delayProperty;
    SerializedProperty positions;

    int size = 0;
    List<Vector3> structurePosition;
    


    private void OnEnable()
    {
        levelManager = target as LevelManager;
        delayProperty = serializedObject.FindProperty("delayBySpawn");
    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        EditorGUI.BeginChangeCheck();


        GUILayout.BeginHorizontal("box");


        size = EditorGUILayout.IntField("Number of Chunks", size);

        if (GUILayout.Button("Create"))
        {
            GenerateChunk(size);
        }


        GUILayout.EndHorizontal();

    }

    void GenerateChunk(int size)
    {
        List<ChunkSetUp> tempCC = new List<ChunkSetUp>();

        string path = "Assets/ChunkConfigs";

        
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets", "ChunkConfigs");
        }




        for (int i = 0; i < size; i++)
        {
            ChunkSetUp chunk = new ChunkSetUp();

            string assetPath = path + "/MyNewConfig.asset";
            assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
            AssetDatabase.CreateAsset(chunk, assetPath);

            levelManager.chunkConfigList.Add(chunk);


            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            EditorGUIUtility.PingObject(chunk);
        }

        
    }
}
