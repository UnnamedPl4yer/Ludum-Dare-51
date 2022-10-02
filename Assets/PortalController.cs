using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public string worldToLoad;
    public GlobalSceneManager globalSceneManager;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            // Debug.Log("Load World " + worldToLoad);
            globalSceneManager.LoadSceneByName(worldToLoad);
        }
    }

    void Update() {
        float rotateDegrees = Random.Range(0.3f, 0.5f);
        // transform.Rotate(new Vector3(0, 0, rotateDegrees));
        transform.position = new Vector3(transform.position.x, 0, transform.position.z) + 0.25f * Vector3.up * Mathf.Cos(Time.time);
    }
}
