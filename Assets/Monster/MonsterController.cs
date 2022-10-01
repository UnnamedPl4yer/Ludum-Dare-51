using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
 {
    public Transform playerTransform;
    private AIPath aiPath;

    public float moveSpeed = 6.0f;
    private float sleepTime = 10.0f; // 10 seconds
    [SerializeField] private bool canMove = false;

    // Start ist called once
    void Start() {
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = moveSpeed;
        aiPath.canMove = canMove;
        aiPath.endReachedDistance = 1.0f;
        StartCoroutine(WaitUntilCanMove());
    }

    // Update is called once per frame
    void Update() {
        aiPath.canMove = canMove;
        if (!canMove) return;

        Debug.Log("Distance to player: " + aiPath.remainingDistance);
        Gizmos.DrawLine(transform.position, destination);
        aiPath.Move(aiPath.desiredVelocity * moveSpeed * Time.deltaTime);
        if (aiPath.reachedDestination) {
            canMove = false;
            StartCoroutine(WaitUntilCanMove());
        }
    }

    IEnumerator WaitUntilCanMove() {
        yield return new WaitForSeconds(sleepTime);
        canMove = true;
        aiPath.destination = playerTransform.position;
    }
}
