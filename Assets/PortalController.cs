using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public string worldToLoad;
    public GlobalSceneManager globalSceneManager;

    private float randomStart;
    private float originPosY;

    void Start(){
        originPosY = transform.position.y;
    }
    
    void Update() {
        Vector3 rotation = 0.01f * Vector3.forward * Mathf.Cos(Time.time);
        float savedPosY = transform.position.y;
        transform.Rotate(rotation);
        Vector3 translation = new Vector3(transform.position.x, originPosY, transform.position.z) + 0.2f * Vector3.up * (Mathf.Cos(Time.time) + 1) / 2;
        //translation.y = savedPosY;
        transform.position = translation;
        // transform.position = new Vector3(transform.position.x, 0, transform.position.z) + 0.25f * Vector3.up * Mathf.Cos(randomStart);
        // randomStart += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            // Debug.Log("Load World " + worldToLoad);
            globalSceneManager.LoadSceneByName(worldToLoad);
        }
    }
}
