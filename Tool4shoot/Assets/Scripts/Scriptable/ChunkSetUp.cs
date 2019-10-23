using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ChunkSetUp : ScriptableObject
{
    public enum ObjectIDs { Nothing, Ennemy, Wall, Collectible };

    public ObjectIDs[] objectId = new ObjectIDs[24];
}




