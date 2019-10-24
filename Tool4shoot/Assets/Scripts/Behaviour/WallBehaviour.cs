using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public Transform poolTransformParent;
    public bool isAvailable;


    private void Awake()
    {
        isAvailable = true;
    }

    private void Update()
    {
        if (transform.position.x < -4f)
        {
            this.transform.parent = poolTransformParent;
            isAvailable = true;
            gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
        GameManager.Instance.ReloadScene();
    }
}
