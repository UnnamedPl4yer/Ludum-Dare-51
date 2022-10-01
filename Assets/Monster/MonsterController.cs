using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
 {
    public Transform playerTransform;
    public Rigidbody2D rb;
    public AIPath aiPath;
    private GridGraph aiGrid;

    [SerializeField] private PlayerAir playerAirComponent;
    [SerializeField] private Vector3 targetingInaccuracyRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sleepTime; // seconds
    [SerializeField] private bool canMove = false;
    private float timeSinceCanMove = 0;
    private bool emergencyStop = false;

    // Start ist called once
    void Start() {
        aiGrid = AstarPath.active.data.gridGraph;
        aiPath.maxSpeed = moveSpeed;
        aiPath.endReachedDistance = 0.1f;
        StartCoroutine(WaitUntilCanMove());
    }

    // Update is called once per frame
    void Update() {
        MoveAI();
        // AI is stuck somewhere
        // if (new Vector2(rb.velocity.x, rb.velocity.y).magnitude <= 0.01f && !aiPath.reachedDestination && timeSinceCanMove > 1.0f) {
        //     emergencyStop = true;
        //     Debug.Log("EMERGENCY STOP!");
        // }
    }

    void MoveAI() {
        aiPath.canMove = canMove;
        if (!canMove) return;
        timeSinceCanMove += Time.deltaTime;

        // Debug.Log("Distance to player: " + aiPath.remainingDistance);
        aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
        if (aiPath.reachedDestination || emergencyStop) {
            canMove = false;
            emergencyStop = false;
            StartCoroutine(WaitUntilCanMove());
        }
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
        yield return new WaitForSeconds(sleepTime);
        canMove = true;
        timeSinceCanMove = 0;
        aiPath.destination = GetDestination();
    }

    Vector3 GetDestination() {
        // Radius around player
        float breathMultiplier = playerAirComponent.currentBreathMultiplier;
        Vector3 targetingInaccuracy = new Vector3(
            Random.Range(-targetingInaccuracyRange.x, targetingInaccuracyRange.x),
            Random.Range(-targetingInaccuracyRange.y, targetingInaccuracyRange.y),
            0
        );
        // GraphNode near chosen random location
        GraphNode node = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy * breathMultiplier).node;
        while (!node.Walkable) {
            node = AstarPath.active.GetNearest(playerTransform.position + targetingInaccuracy * breathMultiplier).node;
        } 
        return (Vector3)(node.position);
    }
}
