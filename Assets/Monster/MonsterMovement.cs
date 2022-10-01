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

    // Far tracking
    [SerializeField] private PlayerAir playerAirComponent;
    [SerializeField] private PlayerAir playerStealthComponent;
    [SerializeField] private Vector3 targetingInaccuracyRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sleepTime; // seconds
    [SerializeField] private bool canMove = false;
    
    // Near tracking
    [SerializeField] private bool isHunting = false;

    // UI
    [SerializeField] private TimerDisplay td;

    // Start ist called once
    void Start() {
        aiGrid = AstarPath.active.data.gridGraph;
        aiPath.maxSpeed = moveSpeed;
        aiPath.endReachedDistance = 0.1f;
        StartCoroutine(WaitUntilCanMove());
    }

    // Update is called once per frame
    void Update() {
        if (!isHunting) {
            StalkPlayer();
        } else {
            HuntPlayer();
        }
    }

    void StalkPlayer() {
        aiPath.canMove = canMove;
        if (!canMove) return;

        // Debug.Log("Distance to player: " + aiPath.remainingDistance);
        aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
        if (aiPath.reachedDestination) {
            canMove = false;
            StartCoroutine(WaitUntilCanMove());
        }
    }

    void HuntPlayer() {
        aiPath.canMove = isHunting;
        aiPath.destination = playerTransform.position;
        aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
    }

    // Debug stuff for visualization
    void OnDrawGizmos() {
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
        td.StartTimer(sleepTime);
        yield return new WaitForSeconds(sleepTime);
        canMove = true;
        aiPath.destination = GetDestinationNearPlayer();
    }

    Vector3 GetDestinationNearPlayer() {
        // Radius around player
        float breathMultiplier = playerAirComponent.currentBreathMultiplier;
        float stealthMultiplier = playerStealthComponent.currentHidingMultiplier;

        Vector3 targetingInaccuracy = new Vector3(
            Random.Range(-targetingInaccuracyRange.x, targetingInaccuracyRange.x),
            Random.Range(-targetingInaccuracyRange.y, targetingInaccuracyRange.y),
            0
        );
        // GraphNode near chosen random location
        GraphNode node = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy * breathMultiplier * stealthMultiplier).node;
        while (!node.Walkable) {
            node = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy * breathMultiplier * stealthMultiplier).node;
        } 
        return (Vector3)(node.position);
    }

    // Player located - start the hunt
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") isHunting = true;
    }

    // Player too far away - stop the hunt
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") isHunting = false;
    }

    // Stop movement when player is caught
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            moveSpeed = 0.0f;
            aiPath.maxSpeed = 0.0f;
        }
    }
}
