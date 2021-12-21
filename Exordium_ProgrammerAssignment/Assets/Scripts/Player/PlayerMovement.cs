using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.4f;
    public Rigidbody2D Rb;
    public Animator animator;
    Vector2 movement;

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
    }
}
