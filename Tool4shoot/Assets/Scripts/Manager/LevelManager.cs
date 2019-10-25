using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn Delay")]
    public float delayBySpawn = 1;
    private float currentDelay;

    private Vector3 startPosition = new Vector3(15f, 0, 0);
    private Vector3 wallScale = new Vector3(1, 5, 1.5f);
    //private Vector3 

    [Header("Chunk Config List")]
    public ChunkSetUp[] chunkConfigList;
    private Vector3[] chunkPositions = new Vector3[6];



    public static LevelManager Instance;




    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < chunkPositions.Length; i++)
        {
            float z = -3.75f;
            chunkPositions[i] = new Vector3(0, 0, z + 1.5f*i);
        }
    }


    void Update()
    {
        // if (Time.time % delayBySpawn < Time.deltaTime) SpawnNextChunk();


        if (currentDelay < delayBySpawn)
        {
            currentDelay += Time.deltaTime * GameManager.Instance.gameSpeedModifier;
        }
        else
        {
            currentDelay = 0;
            SpawnNextChunk();
        }
    }

    void SpawnNextChunk()
    {
        GameObject obj = StructurePooling.Instance.GetStructurePooled();

        if (obj == null)
            return;

        obj.transform.position = startPosition;
        ChunkLevelBehaviour objChunk = obj.GetComponent<ChunkLevelBehaviour>();


        int configIndex = Random.Range(0, chunkConfigList.Length);
        //Debug.Log("Selected config : " + configIndex);
        int currentPosition = 0;

        for (int i = 0; i < chunkConfigList[configIndex].objectId.Length; i++)
        {
            //Debug.Log(i);
            //Debug.Log(chunkConfigList[configIndex].objectId[i]);


            if (chunkConfigList[configIndex].objectId[i] == ChunkSetUp.ObjectIDs.Wall)
            {
                GameObject wall = WallPooling.Instance.GetWallPooled();


                if (wall != null)
                {
                    if (i < 6)
                        wall.transform.parent = objChunk.upParent;
                    else if (i < 12)
                        wall.transform.parent = objChunk.frontParent;
                    else if (i < 18)
                        wall.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        wall.transform.parent = objChunk.downParent;


                    wall.transform.localPosition = chunkPositions[currentPosition];
                    wall.transform.localEulerAngles = new Vector3(0, 0, 0);
                    wall.transform.localScale = wallScale;
                    wall.SetActive(true);
                }
                
            }

            if (chunkConfigList[configIndex].objectId[i] == ChunkSetUp.ObjectIDs.Ennemy)
            {
                GameObject ennemy = EnnemyPooling.Instance.GetEnnemyPooled();

                if (ennemy != null)
                {
                    if (i < 6)
                        ennemy.transform.parent = objChunk.upParent;
                    else if (i < 12)
                        ennemy.transform.parent = objChunk.frontParent;
                    else if (i < 18)
                        ennemy.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        ennemy.transform.parent = objChunk.downParent;


                    ennemy.transform.localPosition = chunkPositions[currentPosition];
                    ennemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                    //ennemy.transform.localScale = new Vector3(1,1,1);

                    ennemy.SetActive(true);
                }
               
            }

            if (chunkConfigList[configIndex].objectId[i] == ChunkSetUp.ObjectIDs.Collectible)
            {
                GameObject col = CollectiblePooling.Instance.GetCollectiblePooled();

                if (col != null)
                {
                    if (i < 6)
                        col.transform.parent = objChunk.upParent;
                    else if (i < 12)
                        col.transform.parent = objChunk.frontParent;
                    else if (i < 18)
                        col.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        col.transform.parent = objChunk.downParent;


                    col.transform.localPosition = chunkPositions[currentPosition];
                    col.transform.localEulerAngles = new Vector3(20,-70,0);
                    //col.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    col.SetActive(true);
                }
              
            }

            currentPosition ++;

            if (currentPosition > 5)
                currentPosition = 0;

        }


        obj.SetActive(true);
    }
}
