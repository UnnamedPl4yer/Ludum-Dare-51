using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAir : MonoBehaviour
{
    private Rigidbody2D rb;

    // Stealth - Stop breathing
    [SerializeField] private int maxAir;
    [SerializeField] private int airDecreaseRate; // per second
    [SerializeField] private int airIncreaseRate; // per second
    [SerializeField] private int chokingThreshold;
    [SerializeField] private float airMoveMultiplier; // x increase while moving
    [SerializeField] private float currentAir;

    public float targetingInaccuracyWhileChoking;
    public float targetingInaccuracyWhileHoldingBreath;
    public float currentBreathMultiplier;
    [SerializeField] private bool isChoking = false;
    [SerializeField] private bool isHoldingBreath = false;

    // UI
    public AirMeterController airMeterController;

    // Called only once after GameObject initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentAir = maxAir;
        airMeterController.maxAir = maxAir;
        airMeterController.chokingThreshold = chokingThreshold;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey("e") && !isChoking) {
            DecreaseAir();
            isHoldingBreath = true; 
            currentBreathMultiplier = targetingInaccuracyWhileHoldingBreath;
        } else {
            IncreaseAir();
            isHoldingBreath = false;
            currentBreathMultiplier = isChoking ? targetingInaccuracyWhileChoking : 1.0f;
        }
        airMeterController.remainingAir = currentAir;
    }

    // Decrease air if player presses key and is not choking
    // Increase speed of loss while walking or running
    private void DecreaseAir() {
        float moveMultiplier = rb.velocity.magnitude > 0.1f ? airMoveMultiplier : 1;
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
