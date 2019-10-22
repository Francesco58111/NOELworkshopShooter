using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class ChunkInspector : Editor
{
    public ChunkSetUp[] chunks;

    LevelManager lvl;


    private void OnEnable()
    {
        lvl = target as LevelManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
