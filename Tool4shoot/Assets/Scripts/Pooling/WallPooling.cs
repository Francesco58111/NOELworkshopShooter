using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPooling : MonoBehaviour
{
    [Header("Pooling System")]
    public GameObject pooledWall;
    public int poolWallAmount;

    List<GameObject> pooledWalls;

    public static WallPooling Instance;




    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledWalls = new List<GameObject>();

        for (int i = 0; i < poolWallAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledWall, this.transform);
            obj.GetComponent<WallBehaviour>().poolTransformParent = this.transform;
            obj.SetActive(false);
            pooledWalls.Add(obj);
        }
    }

    public GameObject GetWallPooled()
    {
        for (int i = 0; i < pooledWalls.Count; i++)
        {
            if (!pooledWalls[i].activeInHierarchy && pooledWalls[i].GetComponent<WallBehaviour>().isAvailable)
            {
                pooledWalls[i].GetComponent<WallBehaviour>().isAvailable = false;
                return pooledWalls[i];
            }
        }

        return null;
    }
}
