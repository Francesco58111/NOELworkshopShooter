using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(LevelManager))]
public class LevelManagerInspector : Editor
{

    LevelManager levelManager;

    ReorderableList myConfigList;
    SerializedProperty chunkArray;

    SerializedProperty delayProperty;
    SerializedProperty positions;

    int size;
    List<Vector3> structurePosition;



    private void OnEnable()
    {
        levelManager = target as LevelManager;
        chunkArray = serializedObject.FindProperty("chunkConfigList");
        delayProperty = serializedObject.FindProperty("delayBySpawn");

        myConfigList = new ReorderableList(serializedObject, chunkArray, true, true, true, true);

        myConfigList.drawHeaderCallback = myConfigListHeader;
        myConfigList.drawElementCallback = myConfigListElementDrawer;
        myConfigList.onAddCallback = myConfigListAdd;
        myConfigList.onRemoveCallback = myConfigListRemove;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Level Spawn parameters", EditorStyles.boldLabel);

        GUILayout.Space(10);

        GUI.color = Color.black;
        EditorGUILayout.BeginHorizontal("helpbox");
        GUI.color = Color.white;
        delayProperty.floatValue = EditorGUILayout.FloatField("Delay Between each level chunk", delayProperty.floatValue);
        GUI.color = Color.black;
        EditorGUILayout.EndHorizontal();
        GUI.color = Color.white;

        GUILayout.Space(10);
        myConfigList.DoLayoutList();


        EditorGUILayout.EndVertical();


        serializedObject.ApplyModifiedProperties();



        //EditorGUI.BeginChangeCheck();


        //GUILayout.BeginHorizontal("box");


        //size = EditorGUILayout.IntField("Number of Chunks", size);

        //if (GUILayout.Button("Create"))
        //{
        //    GenerateChunk(size);
        //}


        //GUILayout.EndHorizontal();

    }

    void myConfigListHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, "Chunk Configs for the Level");
    }

    void myConfigListElementDrawer(Rect rect, int index, bool isActive, bool isFocused)
    {
        rect.yMin += 1;
        rect.yMax -= 4;
        EditorGUI.PropertyField(rect, chunkArray.GetArrayElementAtIndex(index));
    }

    void myConfigListAdd(ReorderableList reoList)
    {
        string path = "Assets/ChunkConfigs";

        chunkArray.arraySize++;

        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets", "ChunkConfigs");
        }

        ChunkSetUp chunk = new ChunkSetUp();

        string assetPath = path + "/MyNewConfig.asset";
        assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
        AssetDatabase.CreateAsset(chunk, assetPath);

        //levelManager.chunkConfigList.Add(chunk);

        chunkArray.GetArrayElementAtIndex(chunkArray.arraySize - 1).objectReferenceValue = chunk;

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();

        EditorGUIUtility.PingObject(chunk);



    }

    void myConfigListRemove(ReorderableList reoList)
    {
        chunkArray.DeleteArrayElementAtIndex(reoList.index);
    }

    #region oldStuff
    //void GenerateChunk(int size)
    //{
    //    List<ChunkSetUp> tempCC = new List<ChunkSetUp>();

    //    string path = "Assets/ChunkConfigs";


    //    if (!AssetDatabase.IsValidFolder(path))
    //    {
    //        AssetDatabase.CreateFolder("Assets", "ChunkConfigs");
    //    }




    //    for (int i = 0; i < size; i++)
    //    {
    //        ChunkSetUp chunk = new ChunkSetUp();

    //        string assetPath = path + "/MyNewConfig.asset";
    //        assetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);
    //        AssetDatabase.CreateAsset(chunk, assetPath);

    //        //levelManager.chunkConfigList.Add(chunk);


    //        AssetDatabase.Refresh();
    //        AssetDatabase.SaveAssets();

    //        EditorGUIUtility.PingObject(chunk);
    //    }


    //}
    #endregion
}
