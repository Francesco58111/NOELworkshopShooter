using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehaviour : MonoBehaviour
{
    public GameObject chunkRef;

    public bool isRotatingLeft;
    public bool isRotatingRight;
    public bool isTargetSet;

    public Vector3 currentRotation;
    public Vector3 targetRotation;


    public AnimationCurve rotationCurve;
    public float currentRotationTime;
    public float percent;

    public float levelSpeedModifier = 0;



    public static CoreBehaviour Instance;




    private void Awake()
    {
        Instance = this;

        currentRotation = new Vector3(transform.rotation.eulerAngles.x, 0, 0);
    }


    private void Update()
    {
        if (isRotatingRight)
        {
            RotateLevel(90);
            RotationCurve();
        }

        if (isRotatingLeft)
        {
            RotateLevel(-90);
            RotationCurve();
        }
    }

    void RotationCurve()
    {
        if (currentRotationTime < 1)
        {
            currentRotationTime += Time.deltaTime;

            percent = rotationCurve.Evaluate(currentRotationTime);
        }
        else
        {
            isRotatingLeft = false;
            isRotatingRight = false;

            isTargetSet = false;


            percent = 0;
            currentRotationTime = 0;


            currentRotation = new Vector3(transform.rotation.eulerAngles.x, 0, 0);
        }
    }

    public void RotateLevel(float tilt)
    {
        if (!isTargetSet)
        {
            isTargetSet = true;
            targetRotation = new Vector3(currentRotation.x + tilt, currentRotation.y, currentRotation.z);

            Debug.Log("current Rotation = " + currentRotation);
            Debug.Log("target Rotation = " + targetRotation);
        }
        else
        {
            transform.localEulerAngles = Vector3.Lerp(currentRotation, targetRotation, percent);
        }

        #region Quaternion
            /*
            if (!isTargetSet)
            {
                isTargetSet = true;
                targetRotation = Quaternion.Euler(currentRotation.x + tilt, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, percent);

            }
            */
            #endregion

    }


}
