using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePooling : MonoBehaviour
{
    [Header("Pooling System")]
    public GameObject pooledStructure;
    public int poolStructureAmount;

    List<GameObject> pooledStructures;


    public Vector3[] structureCases = new Vector3[24];




    public static StructurePooling Instance;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledStructures = new List<GameObject>();

        for (int i = 0; i < poolStructureAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledStructure, this.transform);
            obj.SetActive(false);
            pooledStructures.Add(obj);
        }
    }

    public GameObject GetStructurePooled()
    {
        for (int i = 0; i < pooledStructures.Count; i++)
        {
            if (!pooledStructures[i].activeInHierarchy)
            {
                return pooledStructures[i];
            }
        }

        return null;
    }
}
