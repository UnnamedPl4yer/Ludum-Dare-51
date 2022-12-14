using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GlobalSceneManager sceneManager;
    public GameStats gameStats;
    public int myObjective;

    public void PickUp() {
        gameStats.collectibles[myObjective] = true;
        gameStats.completedLevels[myObjective] = true;
        gameObject.SetActive(false);
        // sceneManager.LoadHubScene();
    }

    // public void OnTriggerEnter2D(Collider2D col) {
    //     if (col.gameObject.tag == "Player") {
    //         gameStats.collectibles[myObjective] = true;
    //         gameStats.completedLevels[myObjective] = true;
    //         sceneManager.LoadHubScene();
    //     }
    // }
}
