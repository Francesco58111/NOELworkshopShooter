using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QuestManager : MonoBehaviour
{
    [Header("Récupérationdes components")]
    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI goalTMP;


    public List<QuestReader> missionsDifficulty;

    private float goal;
    private string title;
    private float questReward;

    private ConditionType currentConditionType;

    [Header("Missions Update")]
    [Tooltip("How much time before the mission update to a new one ?")] public float timeBeforeNextMissionAssignement;

    [Header("Speed Modifier for Distance related missions")]
    [Range(3,10)][Tooltip("How fast the distance value drop ?")]public float distanceModifier;
    private bool timeIsNeeded = false;

    public static QuestManager Instance;




    private void Awake()
    {
        Instance = this;

        StartCoroutine(NextQuest());
    }

    private void Update()
    {
        if (timeIsNeeded)
        {
            UpdateGoal(CheckQuestForValue(Time.deltaTime * distanceModifier, ConditionType.Distance));
        }
    }


    public float CheckQuestForValue(float rewardValue, ConditionType targetCondition)
    {
        if (targetCondition == currentConditionType)
        {
            return goal -= rewardValue;
        }

        return goal;
    }

    public void UpdateGoal(float goalValue)
    {
        goalTMP.text = GoalDislay(goalValue);

        if (goal == 0)
        {
            goalTMP.text = "Mission completed";
            GameManager.Instance.AddToScore(questReward);
            StartCoroutine(NextQuest());
        }
    }

    string GoalDislay(float goalValue)
    {
        switch (currentConditionType)
        {
            case ConditionType.Distance:
                {
                    titleTMP.text = title;
                    return "Parcourir " + goalValue.ToString() + "m";
                }

            case ConditionType.Kill:
                {
                    titleTMP.text = title;
                    return "Percuter " + goalValue.ToString() + " ennemies";
                }

            case ConditionType.Gold:
                {
                    titleTMP.text = title;
                    return "Collecter " + goalValue.ToString() + " pièces d'or";
                }

            case ConditionType.Exploration:
                {
                    titleTMP.text = title;
                    return "Tourner le level " + goalValue.ToString() + " fois";
                }

        }

        return "";
    }

    IEnumerator NextQuest()
    {
        int nextQuest = Random.Range(0, missionsDifficulty[0].missions.Length - 1);

        title = missionsDifficulty[0].missions[nextQuest].title;
        currentConditionType = missionsDifficulty[0].missions[nextQuest].ConditionType;
        goal = missionsDifficulty[0].missions[nextQuest].condition;
        questReward = missionsDifficulty[0].missions[nextQuest].reward;

        yield return new WaitForSeconds(timeBeforeNextMissionAssignement);

        goalTMP.text = GoalDislay(goal);
        titleTMP.text = title;

        if (currentConditionType == ConditionType.Distance)
        {
            timeIsNeeded = true;
        }
        else
        {
            timeIsNeeded = false;
        }
    }


}
