using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSetUp : ScriptableObject
{
    public enum objectID { Nothing, Ennemy, Wall, Collectible };

    public Vector3[] objectPosition;
    public objectID[] objectId;



}
