using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveVector;
    
    private Rigidbody2D rb;
    private Animator animator;

    private PlayerAudioController audioController;

    private bool lastMovedRight = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = GetComponent<PlayerAudioController>();
    }

    void Update() {
        if (Input.GetMouseButton(1)) {
            // animator.SetBool("igniting", true);
            moveVector = new Vector2(0, 0);
            return;
        }
        GetMovement();
        if (moveVector.magnitude >= 0.1f) {
            audioController.PlayStepSound();
        }
        // animator.SetBool("igniting", false);
        animator.SetBool("moving", moveVector.magnitude > 0.0f);
        float localScaleX = transform.localScale.x;
        if (moveVector.x > 0.0f || lastMovedRight) {
            transform.localScale = new Vector2( -1 * Mathf.Abs(localScaleX), transform.localScale.y);
        } else {
            transform.localScale = new Vector2(Mathf.Abs(localScaleX), transform.localScale.y);
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
        if (moveVector.x > 0) lastMovedRight = true;
        if (moveVector.x < 0) lastMovedRight = false;
    }
}
