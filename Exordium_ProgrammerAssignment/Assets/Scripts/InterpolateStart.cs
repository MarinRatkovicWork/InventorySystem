using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateStart : MonoBehaviour
{
    public Camera camera;
    private void OnTriggerEnter2D(Collider2D collider) {

        if(collider.gameObject.name == "Player")
        {

            camera.GetComponent<CameraMenager>().trigerObject = this.gameObject;
            camera.GetComponent<CameraMenager>().loopNumber = 0;

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            camera.GetComponent<CameraMenager>().cameraMode = 2;
           
            this.gameObject.SetActive(false);
            

         
        }
                  
    }
}
