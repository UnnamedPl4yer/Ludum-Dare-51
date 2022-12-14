using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxMoveSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    private Vector2 moveVector;
    
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private GameObject interactInfo;
    private bool canInteractWithTarget = false;
    private bool canInteractWithNPC = false;
    private TargetController targetObject;
    private OldManController npcObject;

    private PlayerAudioController audioController;

    private bool lastMovedRight = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioController = GetComponent<PlayerAudioController>();
        interactInfo.SetActive(false);
    }

    void Update() {
        if (Input.GetMouseButton(1)) { // igniting torch
            // animator.SetBool("igniting", true);
            moveSpeed = maxMoveSpeed / 4;
        } else {
            moveSpeed = maxMoveSpeed;
        }

        // Movement
        GetMovement();
        if (moveVector.magnitude >= 0.1f) {
            audioController.PlayStepSound();
        }
        
        // Interations
        if (canInteractWithNPC && Input.GetKeyDown(KeyCode.F)) {
            npcObject.SpeakNextDialogue();
        }
        if (canInteractWithTarget && Input.GetKeyDown(KeyCode.F)) {
            targetObject.PickUp();
            audioController.PlayPickUpSound();
        }

        // animator.SetBool("igniting", false);
        animator.SetBool("moving", moveVector.magnitude > 0.0f);
        float localScaleX = transform.localScale.x;
        if (moveVector.x > 0.0f || lastMovedRight) {
            transform.localScale = new Vector2( -1 * Mathf.Abs(localScaleX), transform.localScale.y);
        } else {
            transform.localScale = new Vector2(Mathf.Abs(localScaleX), transform.localScale.y);
        }
        if (transform.localScale.x < 0) {
            interactInfo.transform.localScale = new Vector3(-0.5f, 0.5f, -0.5f);
        } else {
            interactInfo.transform.localScale = new Vector3(0.5f, 0.5f, -0.5f);
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

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Target") {
            interactInfo.SetActive(true);
            canInteractWithTarget = true;
            targetObject = col.gameObject.GetComponent<TargetController>();
        }
        if (col.gameObject.tag == "NPC") {
            interactInfo.SetActive(true);
            canInteractWithNPC = true;
            npcObject = col.gameObject.GetComponent<OldManController>();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Target") {
            interactInfo.SetActive(false);
            canInteractWithTarget = false;
            targetObject = null;
        }
        if (col.gameObject.tag == "NPC") {
            interactInfo.SetActive(false);
            canInteractWithNPC = false;
            npcObject = null;
        }
    }
}
