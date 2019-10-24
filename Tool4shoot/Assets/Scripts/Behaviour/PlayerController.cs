using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float smoothStrength;
    public float smoothTime = 0.15f;

    public Transform objectToFollow;
    private Vector3 refVector;

    public float playerSpeed;



    private void Update()
    {
        if (!CoreBehaviour.Instance.isRotatingLeft && !CoreBehaviour.Instance.isRotatingRight)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                CoreBehaviour.Instance.isRotatingLeft = true;

            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                CoreBehaviour.Instance.isRotatingRight = true;
            }
        }

        Move();

    }

    void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, objectToFollow.position, ref refVector, smoothTime, smoothStrength);

        if (Input.GetKey(KeyCode.UpArrow) && objectToFollow.position.y < 3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y + Time.deltaTime * playerSpeed, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow) && objectToFollow.position.y > -3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y - Time.deltaTime * playerSpeed, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && objectToFollow.position.x > -1f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x - Time.deltaTime * playerSpeed, objectToFollow.position.y, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.RightArrow) && objectToFollow.position.x < 8f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x + Time.deltaTime * playerSpeed, objectToFollow.position.y, objectToFollow.position.z);
        }
    }
}
