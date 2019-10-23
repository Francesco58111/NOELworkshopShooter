using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Spawn Delay")]
    public float delayBySpawn;
    private float currentDelay;

    private Vector3 startPosition = new Vector3(0.7f, 0, 0);

    [Header("Chunk Config List")]
    public List<ChunkSetUp> chunkConfigList = new List<ChunkSetUp>();
    public Vector3[] chunkPositions;



    public static LevelManager Instance;




    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {

    }

    void Update()
    {
        if (currentDelay < delayBySpawn)
        {
            currentDelay += Time.deltaTime;
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


        int configIndex = Random.Range(0, chunkConfigList.Count);


        for (int i = 0; i < chunkConfigList[configIndex].objectId.Length; i++)
        {
            switch (chunkConfigList[configIndex].objectId[i])
            {
                case ChunkSetUp.ObjectIDs.Nothing:
                    break;


                case ChunkSetUp.ObjectIDs.Ennemy:
                    GameObject ennemy = EnnemyPooling.Instance.GetEnnemyPooled();

                    if (ennemy == null)
                        return;

                    if (i == 0 && i < 6)
                        ennemy.transform.parent = objChunk.upParent;
                    else if (i > 5 && i < 12)
                        ennemy.transform.parent = objChunk.frontParent;
                    else if (i > 11 && i < 18)
                        ennemy.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        ennemy.transform.parent = objChunk.downParent;

                    ennemy.transform.localPosition = chunkPositions[i];
                    ennemy.SetActive(true);
                    break;


                case ChunkSetUp.ObjectIDs.Wall:
                    GameObject wall = WallPooling.Instance.GetWallPooled();

                    if (wall == null)
                        return;

                    if (i == 0 && i < 6)
                        wall.transform.parent = objChunk.upParent;
                    else if (i > 5 && i < 12)
                        wall.transform.parent = objChunk.frontParent;
                    else if (i > 11 && i < 18)
                        wall.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        wall.transform.parent = objChunk.downParent;

                    wall.transform.localPosition = chunkPositions[i];
                    wall.SetActive(true);
                    break;


                case ChunkSetUp.ObjectIDs.Collectible:
                    GameObject col = CollectiblePooling.Instance.GetCollectiblePooled();

                    if (col == null)
                        return;

                    if (i == 0 && i < 6)
                        col.transform.parent = objChunk.upParent;
                    else if (i > 5 && i < 12)
                        col.transform.parent = objChunk.frontParent;
                    else if (i > 11 && i < 18)
                        col.transform.parent = objChunk.backParent;
                    else if (i > 17)
                        col.transform.parent = objChunk.downParent;

                    col.transform.localPosition = chunkPositions[i];
                    col.SetActive(true);
                    break;
            }
        }
        
    
        obj.SetActive(true);
    }
}
