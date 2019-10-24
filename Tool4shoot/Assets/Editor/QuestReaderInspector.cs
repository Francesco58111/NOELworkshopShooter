using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestReader))]
public class QuestReaderInspector : Editor
{
    QuestReader assetTarget;

    private void OnEnable()
    {
        assetTarget = target as QuestReader;
    }

    public override void OnInspectorGUI()
    {
        if (assetTarget.myCSV != null)
        {
            if (GUILayout.Button("Analyze CSV File"))
            {
                GenerateQuests();
            }
        }

        base.OnInspectorGUI();
    }

    void GenerateQuests()
    {
        //Debug.Log("Analizing");

        Undo.RecordObject(assetTarget, "Undo recording");

        string rawContent = assetTarget.myCSV.text;


        string[] lines = rawContent.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        string[] separator = new string[] { ";" };

        string[] conditionType = System.Enum.GetNames(typeof(ConditionType));


        List<QuestSettings> tempQS = new List<QuestSettings>();

        //Debug.Log(lines.Length);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(separator, System.StringSplitOptions.None);


            QuestSettings mission = new QuestSettings();


            int questIndex = 0;
            int.TryParse(cells[0], out questIndex);
            mission.index = questIndex;


            mission.title = cells[1];
            mission.description = cells[2];


            string conditionName = cells[3];
            mission.ConditionType = (ConditionType)System.Enum.Parse(typeof(ConditionType), conditionName, true);


            float goal = 0;
            float.TryParse(cells[4], out goal);
            mission.condition = goal;

            float rewardValue = 0;
            float.TryParse(cells[5], out rewardValue);
            mission.reward = rewardValue;


            tempQS.Add(mission);
        }


        assetTarget.missions = tempQS.ToArray();

    }
}

