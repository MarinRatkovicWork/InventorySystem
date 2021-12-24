
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.4f;
    public Rigidbody2D Rb;
    public Animator animator;
    Vector2 movement;


    GameObject player;
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 currentPosition;
    private float totalDistance;
    public int totalDistanceInt;

    void Start()
    {
        startPosition = gameObject.transform.position;
        currentPosition = gameObject.transform.position;
        endPosition = gameObject.transform.position;
        totalDistance = 0;
    }
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);      
    }

    void FixedUpdate()
    {
        
        Rb.MovePosition(Rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        currentPosition = gameObject.transform.position;
        PlayerWallkingDistence();
        totalDistanceInt = System.Convert.ToInt32(totalDistance);
        

    }

    public float PlayerWallkingDistence()
    {
     

        if (startPosition != currentPosition)        
        {
            
            endPosition = currentPosition;           
            totalDistance = totalDistance + Vector3.Distance(startPosition, endPosition);
            startPosition = currentPosition;
        }

        
        return totalDistance;
    }
}
