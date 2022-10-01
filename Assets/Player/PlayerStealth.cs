using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private bool hiddenInBush = false;
    public float targetingInaccuracyWhileMovingInBush;
    public float targetingInaccuracyWhileStillInBush;
    public float currentHidingMultiplier;

    public float movementSpeedInBushDelimiter;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (hiddenInBush) {
            rb.velocity *= movementSpeedInBushDelimiter;

            if (rb.velocity.magnitude > 0.1f) {
                currentHidingMultiplier = targetingInaccuracyWhileMovingInBush;
            } else {
                currentHidingMultiplier = targetingInaccuracyWhileStillInBush;
            }
        } else {
            currentHidingMultiplier = 1.0f;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "bush") {
            hiddenInBush = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "bush") {
            hiddenInBush = false;
        }
    }
}
