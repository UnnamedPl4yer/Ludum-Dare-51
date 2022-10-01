using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    private Rigidbody2D rb;

    // Stealth - Stop breathing
    public int maxAir = 100;
    public int airDecreaseRate = 10; // per second
    public int airIncreaseRate = 5; // per second
    public int chokingThreshold = 20;
    public float airMoveMultiplier = 1.2f; // x increase while moving
    [SerializeField] private float currentAir = 100;
    [SerializeField] private bool isChoking = false;

    // Called only once after GameObject initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("e") && !isChoking) {
            DecreaseAir();
        } else {
            IncreaseAir();
        }
    }

    // Decrease air if player presses key and is not choking
    // Increase speed of loss while walking or running
    private void DecreaseAir() {
        float moveMultiplier = Mathf.Abs(rb.velocity.x) >= 0.1f || Mathf.Abs(rb.velocity.y) >= 0.1f ? airMoveMultiplier : 1;
        Debug.Log(moveMultiplier);
        currentAir -= (airDecreaseRate * moveMultiplier * Time.deltaTime);
        if (currentAir <= 0) {
            currentAir = 0;
            isChoking = true;
        }
    }

    // Replenish air at a slower rate
    // TODO: use movement as well?
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
