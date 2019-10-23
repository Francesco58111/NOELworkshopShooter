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
        transform.position = Vector3.SmoothDamp(transform.position, objectToFollow.position, ref refVector, smoothTime, followSpeed);

        if (Input.GetKey(KeyCode.UpArrow) && objectToFollow.position.y < 3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y + Time.deltaTime * speed, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.DownArrow) && objectToFollow.position.y > -3.5f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y - Time.deltaTime * speed, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && objectToFollow.position.x > -1f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x - Time.deltaTime * speed, objectToFollow.position.y, objectToFollow.position.z);
        }

        if (Input.GetKey(KeyCode.RightArrow) && objectToFollow.position.x < 8f)
        {
            objectToFollow.position = new Vector3(objectToFollow.position.x + Time.deltaTime * speed, objectToFollow.position.y, objectToFollow.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        GameManager.Instance.ReloadScene();
    }
}
