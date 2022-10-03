using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterMovement : MonoBehaviour
 {
    public Transform playerTransform;
    public Rigidbody2D rb;
    public AIPath aiPath;
    private GridGraph aiGrid;
    private Animator animator;

    // Far tracking
    [SerializeField] private PlayerAir playerAirComponent;
    [SerializeField] private PlayerStealth playerStealthComponent;
    [SerializeField] private Vector3 targetingInaccuracyRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sleepTime; // seconds
    [SerializeField] private bool canMove = false;
    private PlayerAudioController audioController;
    
    // Debug
    private Vector3 debugDestination = Vector3.zero;
    private bool isSearching = false;

    // Near tracking
    [SerializeField] private bool isHunting = false;

    // UI
    public MonsterMeterController monsterMeterController;

    // Start ist called once
    void Start() {
        aiGrid = AstarPath.active.data.gridGraph;
        animator = GetComponent<Animator>();
        audioController = GetComponent<PlayerAudioController>();
        aiPath.maxSpeed = moveSpeed;
        // aiPath.endReachedDistance = 0.1f;
        StartCoroutine(WaitUntilCanMove());
    }

    // Update is called once per frame
    void Update() {
        // Out of bounds fix
        if (transform.position.x < -37f || transform.position.x > 25f || transform.position.y < -21f || transform.position.y > 30f) {
            transform.position = new Vector3(-33f, 25f, 0f); // Reset to original spawn
        }
        // Animations
        animator.SetBool("moving", aiPath.desiredVelocity.magnitude > 0.0f);
        float localScaleX = transform.localScale.x;
        if (aiPath.desiredVelocity.x > 0.0f) {
            transform.localScale = new Vector2( -1 * Mathf.Abs(localScaleX), transform.localScale.y);
        } else {
            transform.localScale = new Vector2(Mathf.Abs(localScaleX), transform.localScale.y);
        }
        // Debug.Log("update 1");
        // Actual movement
        if (!isHunting) {
            // Debug.Log("update 1 stalk");
            StalkPlayer();
        } else {
            // Debug.Log("update 1 hunt");
            HuntPlayer();
        }
        // Debug.Log("update 2");
    }

    void StalkPlayer() {
        // Debug.Log("stalk start");
        aiPath.canMove = canMove;
        if (!canMove) return;
            // Debug.Log("stalk cannot move return " + canMove);
        // Debug.Log("stalk 1");
        // Debug.Log("Distance to player: " + aiPath.remainingDistance);
        if (aiPath.reachedDestination && !isSearching) {
            // Debug.Log("stalk reached destination start");
            canMove = false;
            debugDestination = Vector3.zero;
            // Debug.Log("AI DESTINATION " + aiPath.destination);
            StartCoroutine(WaitUntilCanMove());
            // Debug.Log("stalk reached destination end");
        }
        aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
        if (canMove) audioController.PlayStepSound();
        // Debug.Log("stalk 2");
        // rb.velocity = aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime;
        
        // Debug.Log("stalk end");
    }

    void HuntPlayer() {
        // Debug.Log("hunt start");
        aiPath.canMove = isHunting;
        aiPath.destination = playerTransform.position;
        aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
        audioController.PlayStepSound();
        // rb.velocity = aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime;
        // Debug.Log("hunt end");
    }

    // Debug stuff for visualization
    void OnDrawGizmos() {
        if (debugDestination != Vector3.zero) {
            Gizmos.DrawWireSphere(debugDestination, .5f);
        }
        if (aiPath.hasPath != null) {
            // Debug.Log("AI Destination " + aiPath.destination);
            // Gizmos.DrawLine(transform.position, aiPath.destination);
            // Target location
            Gizmos.DrawWireSphere(aiPath.destination, 0.2f);
            // SearchRadius
            // Debug.Log(targetingInaccuracyRange.x * playerAirComponent.currentBreathMultiplier);
            Gizmos.DrawWireSphere(playerTransform.position, targetingInaccuracyRange.x * playerAirComponent.currentBreathMultiplier);
            // Debug.Log("Current Velocity: " + new Vector2(rb.velocity.x, rb.velocity.y).magnitude);
            Gizmos.DrawLine(transform.position, transform.position + aiPath.desiredVelocity);
        }
    }

    IEnumerator WaitUntilCanMove() {
        isSearching = true;
        monsterMeterController.StartTimer(sleepTime);
        yield return new WaitForSeconds(sleepTime);
        canMove = true;
        aiPath.destination = GetDestinationNearPlayer();
        isSearching = false;
    }

    Vector3 GetDestinationNearPlayer() {
        // Radius around player
        // float breathMultiplier = playerAirComponent.currentBreathMultiplier;
        // float stealthMultiplier = playerStealthComponent.currentHidingMultiplier;

        Vector3 targetingInaccuracy = new Vector3(
            Random.Range(-targetingInaccuracyRange.x, targetingInaccuracyRange.x),
            Random.Range(-targetingInaccuracyRange.y, targetingInaccuracyRange.y),
            0
        );
        // GraphNode near chosen random location
        // Debug.Log("get destination 1");
        GraphNode currentNode = AstarPath.active.GetNearest(transform.position).node;
        GraphNode targetNode = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy).node; // * breathMultiplier * stealthMultiplier).node;
        // Debug.Log("get destination 2");
        // Debug.Log("get destination invalid - Find new node | walkable: " + targetNode.Walkable + " | path possible: " + PathUtilities.IsPathPossible(currentNode, targetNode));
        while ( !(targetNode.Walkable && PathUtilities.IsPathPossible(currentNode, targetNode)) ) {
            // Debug.Log("get destination invalid - Find new node | walkable: " + targetNode.Walkable + " | path possible: " + PathUtilities.IsPathPossible(currentNode, targetNode));
            targetingInaccuracy = new Vector3(
                Random.Range(-targetingInaccuracyRange.x, targetingInaccuracyRange.x),
                Random.Range(-targetingInaccuracyRange.y, targetingInaccuracyRange.y),
                0
            );
            targetNode = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy).node; // * breathMultiplier * stealthMultiplier).node;
        }
        // Debug.Log("get destination 3");
        // Debug.Log( (Vector3)(targetNode.position) );
        // Debug.Log("get destination 4");
        return (Vector3)(targetNode.position);
    }

    // Player located - start the hunt
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            isHunting = true;
            canMove = true;
        }
    }

    // Player too far away - stop the hunt
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            isHunting = false;
            canMove = !isSearching;
        }
    }

    // Stop movement when player is caught
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            moveSpeed = 0.0f;
            aiPath.maxSpeed = 0.0f;
        }
    }
}
