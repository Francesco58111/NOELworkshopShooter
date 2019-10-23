using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePooling : MonoBehaviour
{
    [Header("Pooling System")]
    public GameObject pooledCollectible;
    public int poolCollectibleAmount;

    List<GameObject> pooledCollectibles;


    public static CollectiblePooling Instance;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledCollectibles = new List<GameObject>();

        for (int i = 0; i < poolCollectibleAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledCollectible, this.transform);
            obj.SetActive(false);
            pooledCollectibles.Add(obj);
        }
    }

    public GameObject GetCollectiblePooled()
    {
        for (int i = 0; i < pooledCollectibles.Count; i++)
        {
            if (!pooledCollectibles[i].activeInHierarchy)
            {
                return pooledCollectibles[i];
            }
        }

        return null;
    }
}
