using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GlobalSceneManager sceneManager;
    public GameStats gameStats;
    public int myObjective;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            gameStats.collectibles[myObjective] = true;
            gameStats.completedLevels[myObjective] = true;
            sceneManager.LoadHubScene();
        }
    }
}
