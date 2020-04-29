using UnityEngine;
using System.Collections;

public class ShakeTransformS : MonoBehaviour
{
    public GameObject GameObjectToShake;

    IEnumerator ShakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (decreasePoint >= totalShakeDuration)
        {
            Debug.LogError("decreasePoint must be less than totalShakeDuration...Exiting");
            yield break; //Exit!
        }

        //Get Original rot
        Transform objTransform = objectToShake.transform;
        Quaternion defaultRot = objTransform.rotation;

        float counter = 0f;

        //Shake Speed
        const float speed = 0.1f;

        //Angle Rotation(Optional)
        const float angleRot = 10f;

        //Do the actual shaking
        while (counter < totalShakeDuration)
        {
            counter += Time.deltaTime;
            float decreaseAngle = angleRot;

            //Shake GameObject
            if (objectIs2D)
            {
                //Don't Translate the Z Axis if 2D Object
                //Only Rotate the Z axis if 2D
                objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 20f));
            }
            else
            {
                objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(1f, 1f, 1f));
            }
            yield return null;


            //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
            if (counter >= decreasePoint)
            {
                //Reset counter to 0 
                counter = 0f;
                while (counter <= decreasePoint)
                {
                    counter += Time.deltaTime;
                    decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);
                    //Shake GameObject
                    if (objectIs2D)
                    {
                        //Don't Translate the Z Axis if 2D Object
                        //Only Rotate the Z axis if 2D
                        objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 20f));
                    }
                    else
                    {
                        objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(1f, 1f, 1f));
                    }
                    yield return null;
                }

                //Break from the outer loop
                break;
            }
        }
        objTransform.rotation = defaultRot;//Reset to original rotation
    }


    public void ShakeGameObject(GameObject objectToShake, float shakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        StartCoroutine(ShakeGameObjectCOR(objectToShake, shakeDuration, decreasePoint, objectIs2D));
    }
}