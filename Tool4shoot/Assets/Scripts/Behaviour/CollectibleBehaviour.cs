using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public Transform poolTransformParent;
    public bool isAvailable;

    public float rewardValue;


    private void Awake()
    {
        isAvailable = true;
    }

    private void Update()
    {
        if(transform.position.x < -4f)
        {
            this.transform.parent = poolTransformParent;
            isAvailable = true;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.transform.parent = poolTransformParent;
            QuestManager.Instance.UpdateGoal(QuestManager.Instance.CheckQuestForValue(rewardValue, ConditionType.Gold));
            //GameManager.Instance.AddToScore(1);
            this.gameObject.SetActive(false);
        }
    }
}
