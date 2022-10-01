using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameStats gameStats;
    public GameObject[] doorsToMissions; // Gameobjects to block 

    void Start() {
        for (int i = 0; i < gameStats.completedLevels.Length; i++) {
            //doorsToMissions[i].setActive(false);
        }
    }
}
