using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenager : MonoBehaviour
{
    public Transform player;
    public float speed;
    public Vector3 offset;

    //
    private Vector3 endPosition;
    private Vector3 startPosition;
    private float timeDelay;
    private float elapsedTime;
    public int loopNumber;
    private float stopTimeOnEndObject;
    private bool stopExecution;
    //
    public GameObject trigerObject;
    public int cameraMode;

    [Header("Add interpolate par")]
    public List<CameraInterpolateObjects> Objects;

    void Start()
    {
        stopExecution = false;
        cameraMode = 1;
    }
    void FixedUpdate()
    {
        switch (cameraMode)
        {
            case 1:
                FallowWithDeleyPlayer();               
                break;
            case 2:
                StartInterpolate(trigerObject);              
                break;
            case 3:
                if (stopExecution == false)
                {
                    GoToObjectWithDeleyCamera();
                }
                break;
            default:
                FallowWithDeleyPlayer();
                break;
        }
    }


    private void FallowWithDeleyPlayer()
    {
        Vector3 EndPosition = player.position + offset;
        Vector3 deleyPosition = Vector3.Lerp(transform.position, EndPosition, speed);
        transform.position = deleyPosition;
    }
    private void GoToObjectWithDeleyCamera()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplate = elapsedTime / timeDelay;
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplate));        
        if (transform.position == endPosition)
        {
           StartCoroutine(Wait());
            loopNumber = loopNumber + 1;
            cameraMode = 2;
        }
    }

   private IEnumerator Wait()
    {
        stopExecution = true;
        yield return new WaitForSeconds(stopTimeOnEndObject);
        stopExecution = false;
    }


    private CameraInterpolateObjects FindObjectFromTriggerInterpolaten(GameObject triggerStart)
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            CameraInterpolateObjects interpObject = Objects[i];
            for (int a = 0; a < interpObject.triggerStart.Count; a++)
            {
                if (interpObject.triggerStart[a] == triggerStart)
                {
                    return interpObject;
                }
            }
        }
        return null;
    }

    private List<Vector3> FindInterpolateVectors(CameraInterpolateObjects interpObject)
    {
        List<Vector3> transforms = new List<Vector3>();       
        for (int i = 0; i < interpObject.endObject.Count; i++)
        {
            transforms.Add(interpObject.endObject[i].transform.position);
        }
        return transforms;
    }

    private void Interpolate(CameraInterpolateObjects interpObject, int loopNumber)
    {
        List<Vector3> endVectors = new List<Vector3>();
        endVectors = FindInterpolateVectors(interpObject);
        if(loopNumber+1 > endVectors.Count+1)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            cameraMode = 1;
            
        }
        else if (loopNumber+1 == endVectors.Count+1)
        {
            startPosition = transform.position;
            endPosition = player.position + offset;
            timeDelay = interpObject.speedBetwenStartEndObjects;
            stopTimeOnEndObject = 0; 
            elapsedTime = 0;
            cameraMode = 3;
        }
        else
        {
            for (int i = loopNumber; i < endVectors.Count; i++)
            {
                if (i == 0)
                {
                    startPosition = transform.position;
                    endPosition = endVectors[i] + offset;
                    timeDelay = interpObject.speedBetwenStartEndObjects;
                    stopTimeOnEndObject = interpObject.stopTimeOnEndObject;
                    elapsedTime = 0;
                    cameraMode = 3;
                    break;
                }
                else
                {
                    startPosition = transform.position;
                    endPosition = endVectors[i] + offset;
                    timeDelay = interpObject.speedBetwenEndEndObjects;
                    stopTimeOnEndObject = interpObject.stopTimeOnEndObject;
                    elapsedTime = 0;
                    cameraMode = 3;
                    break;
                }

            }
        }
    }

    private void StartInterpolate(GameObject triggerStart)
    {     
        Interpolate(FindObjectFromTriggerInterpolaten(triggerStart), loopNumber);
        
    }


}
