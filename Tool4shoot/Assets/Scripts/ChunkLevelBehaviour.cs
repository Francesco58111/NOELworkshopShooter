using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLevelBehaviour : MonoBehaviour
{
    public float defaultSpeed = 0.2f;

    public Vector3 target;





    private void Awake()
    {
        
    }

    private void Update()
    {
        OnMove();

        if(transform.localPosition.x < -0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMove()
    {
        transform.localPosition = new Vector3(transform.localPosition.x - (Time.deltaTime * defaultSpeed * LevelManager.Instance.levelSpeedModifier),0,0);
    }

}
