using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<ChunkLevelBehaviour> chunks;

    public bool isRotatingLeft;
    public bool isRotatingRight;
    public bool isTargetSet;

    public Transform currentRotation;
    public Quaternion targetRotation;

    public float smooth;

    public float levelSpeedModifier = 0;



    public static LevelManager Instance;




    private void Awake()
    {
        Instance = this;

        currentRotation.rotation = Quaternion.identity;
    }


    private void Update()
    {
        if (isRotatingRight)
            RotateLevel(90);

        if (isRotatingLeft)
            RotateLevel(-90);
    }


    public void RotateLevel(float tilt)
    {
        if (!isTargetSet)
        {
            isTargetSet = true;
            targetRotation = Quaternion.Euler(transform.rotation.x + tilt, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
        }

        

        if (transform.rotation == targetRotation)
        {
            isRotatingLeft = false;
            isRotatingRight = false;

            isTargetSet = false;

            currentRotation.rotation = Quaternion.identity;
        }
    }
}
