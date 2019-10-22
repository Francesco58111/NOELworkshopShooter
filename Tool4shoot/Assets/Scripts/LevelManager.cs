using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<ChunkLevelBehaviour> chunks;

    public bool isRotatingLeft;
    public bool isRotatingRight;
    public bool isTargetSet;

    public Quaternion currentRotation;
    public Quaternion targetRotation;

    public float smooth;

    public float levelSpeedModifier = 0;



    public static LevelManager Instance;




    private void Awake()
    {
        Instance = this;

        currentRotation = transform.rotation;
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
            targetRotation = Quaternion.Euler(transform.rotation.x + tilt, 0, 0);
            isTargetSet = true;

            StartCoroutine(StopRotationAfter(targetRotation));
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
        }



        //if (transform.rotation == targetRotation)
        //{
        //    isTargetSet = false;

        //    isRotatingLeft = false;
        //    isRotatingRight = false;

        //    currentRotation = transform.rotation;
        //}
    }


    IEnumerator StopRotationAfter(Quaternion target)
    {
        yield return new WaitForSeconds(1f);

        isTargetSet = false;

        isRotatingLeft = false;
        isRotatingRight = false;

        transform.rotation = targetRotation;
    }


}
