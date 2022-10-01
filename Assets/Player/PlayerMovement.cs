using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveVector;
    
    private Rigidbody2D rb;

    // Called only once after GameObject initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once per Frame
    void Update() {
        GetMovement();
    }

    // Called a fixed amount of times per second - Use for physics and rigidbodies!
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
