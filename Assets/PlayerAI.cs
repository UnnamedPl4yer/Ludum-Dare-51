using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private AIPath aiPath;
    private GridGraph aiGrid;
    private Animator animator;

    [SerializeField] private float moveSpeed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        aiGrid = AstarPath.active.data.gridGraph;
        Debug.Log(aiGrid.nodes.Length);
        animator = GetComponent<Animator>();
        aiPath.destination = (Vector3)(aiGrid.nodes[(int)(Random.Range(0, aiGrid.nodes.Length - 1))].position);
    }

    void Update() {
        animator.SetBool("moving", true);

        if (!aiPath.reachedDestination) {
            aiPath.Move(aiPath.desiredVelocity.normalized * moveSpeed * Time.deltaTime);
            
            if (aiPath.desiredVelocity.x > 0f) {
                transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } else {
                transform.localScale = new Vector2(     Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            return;
        }
        int random = Random.Range(0, aiGrid.nodes.Length - 1);
        GraphNode node = aiGrid.nodes[random];
        aiPath.destination = (Vector3)(node.position);
    }
}
