using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{
    public GameObject prefab;
    public float delay;

    public Transform poolTransformParent;
    public bool isAvailable;

    public float rewardValue;


    private void Awake()
    {
        isAvailable = true;
    }

    private void Update()
    {
        if (transform.position.x < -4f)
        {
            this.transform.parent = poolTransformParent;
            isAvailable = true;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.transform.parent = poolTransformParent;
            QuestManager.Instance.UpdateGoal(QuestManager.Instance.CheckQuestForValue(rewardValue, ConditionType.Kill));

            isAvailable = true;
            this.gameObject.SetActive(false);
        }
    }
}
