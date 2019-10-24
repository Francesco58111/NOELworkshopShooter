using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPooling : MonoBehaviour
{
    [Header("Pooling System")]
    public GameObject pooledEnnemy;
    public int poolEnnemyAmount;

    List<GameObject> pooledEnnemies;


    public static EnnemyPooling Instance;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledEnnemies = new List<GameObject>();

        for (int i = 0; i < poolEnnemyAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledEnnemy, this.transform);
            obj.GetComponent<EnnemyBehaviour>().poolTransformParent = this.transform;
            obj.SetActive(false);
            pooledEnnemies.Add(obj);
        }
    }

    public GameObject GetEnnemyPooled()
    {
        for (int i = 0; i < pooledEnnemies.Count; i++)
        {
            if (!pooledEnnemies[i].activeInHierarchy && pooledEnnemies[i].GetComponent<EnnemyBehaviour>().isAvailable)
            {
                pooledEnnemies[i].GetComponent<EnnemyBehaviour>().isAvailable = false;
                return pooledEnnemies[i];
            }
        }

        return null;
    }

}
