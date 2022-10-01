using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public string worldToLoad;
    public GlobalSceneManager globalSceneManager;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            Debug.Log("Load World " + worldToLoad);
            globalSceneManager.LoadSceneByName(worldToLoad);
        }
    }
}
