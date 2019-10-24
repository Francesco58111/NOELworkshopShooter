using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestList.asset", menuName = "Quest/QuestList", order = 100)]
public class QuestReader : ScriptableObject
{
    public TextAsset myCSV;

    public QuestSettings[] missions;
}

    public enum ConditionType { Gold, Distance, Exploration, Time, Kill };


    
    [System.Serializable]
    public struct QuestSettings
    {
        public int index;
        public string title, description;
        public ConditionType ConditionType;
        public float condition;
        public float reward;
    }

