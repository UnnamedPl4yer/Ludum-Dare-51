using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameStats gameStats;
    public GameObject[] portals; // GameObjects to block 


    void Start() {
        for (int i = 1; i < portals.Length - 1; i++) {
            if (gameStats.completedLevels[i - 1]) {
                portals[i].SetActive(true);
            } else {
                portals[i].SetActive(false);
            }
        }
    }

    public void UnlockPortal(int i) {
        portals[i].SetActive(true);
    }

    void UnlockFirstWorld() {
        gameStats.completedLevels[0] = true;
        UnlockPortal(0);
    }
}
