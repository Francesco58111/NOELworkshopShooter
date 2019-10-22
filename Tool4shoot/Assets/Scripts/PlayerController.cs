using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float followSpeed;
    public float smoothTime;

    public Transform objectToFollow;
    private Vector3 refVector;

    public float speed;


    private void Start()
    {

    }

    private void Update()
    {
        if (!LevelManager.Instance.isRotatingLeft && !LevelManager.Instance.isRotatingRight)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LevelManager.Instance.isRotatingLeft = true;

            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                LevelManager.Instance.isRotatingRight = true;
            }
        }

        Move();

    }

    void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, objectToFollow.position, ref refVector, smoothTime, followSpeed);

        if (Input.GetKey(KeyCode.UpArrow) && objectToFollow.position.y < 3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y + Time.deltaTime * speed, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow) && objectToFollow.position.y > -3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y - Time.deltaTime * speed, objectToFollow.position.z);
        }
    }
}
