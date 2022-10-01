using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveVector;
    
    private Rigidbody2D rb;
    private Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        GetMovement();
        animator.SetBool("moving", moveVector.magnitude > 0.0f);
        if (moveVector.x > 0.0f) {
            transform.localScale = new Vector2( -1, transform.localScale.y);
        } else {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    void FixedUpdate() {
        rb.velocity = moveVector * moveSpeed;
    }

    void SetMoveSpeed(float newMoveSpeed) {
        moveSpeed = newMoveSpeed;
    }

    void GetMovement() {
        moveVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }
}
