using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLevelBehaviour : MonoBehaviour
{
    private float defaultSpeed = 0.15f;


    public Transform upParent;
    public Transform frontParent;
    public Transform backParent;
    public Transform downParent;


    private void Update()
    {
        OnMove();

        if (transform.localPosition.x < -0.8f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnMove()
    {
        transform.localPosition = new Vector3(transform.localPosition.x - (Time.deltaTime * defaultSpeed * CoreBehaviour.Instance.levelSpeedModifier), 0, 0);
    }

}
