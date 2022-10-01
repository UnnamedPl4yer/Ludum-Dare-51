using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 300.0f;

    // Stealth - Stop breathing
    public int maxAir = 100;
    public int airDecreaseRate = 10; // per second
    public int airIncreaseRate = 5; // per second
    public int chokingThreshold = 20;
    public float airMoveMultiplier = 1.2f; // x increase while moving
    [SerializeField] private float currentAir = 100;
    [SerializeField] private bool isChoking = false;
    
    private Rigidbody2D rb;

    // Called only once after GameObject initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once per Frame
    void Update() {
        if (Input.GetKey("e") && !isChoking) {
            DecreaseAir();
        } else {
            IncreaseAir();
        }
    }

    // Called a fixed amount of times per second - Use for physics!
    void FixedUpdate() {
        Vector2 moveDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        ).normalized;
        Vector2 force = moveDirection * moveSpeed * Time.deltaTime;
        rb.AddForce(force);
    }

    // Stealth - stop breathing
    private void DecreaseAir() {
        float moveMultiplier = Mathf.Abs(rb.velocity.x) >= 0.1f || Mathf.Abs(rb.velocity.y) >= 0.1f ? airMoveMultiplier : 1;
        Debug.Log(moveMultiplier);
        currentAir -= (airDecreaseRate * moveMultiplier * Time.deltaTime);
        if (currentAir <= 0) {
            currentAir = 0;
            isChoking = true;
        }
    }

    private void IncreaseAir() {
        currentAir += (airIncreaseRate * Time.deltaTime);
        if (currentAir >= maxAir) {
            currentAir = maxAir;
        }
        if (currentAir >= chokingThreshold) {
            isChoking = false;
        }
    }
    // End Stealth - stop breathing
}
