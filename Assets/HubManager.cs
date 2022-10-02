using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    public GameStats gameStats;
    public GameObject[] portals;
    public int waitTime;

    void Start() {
        for (int i = 0; i < portals.Length; i++) {
            portals[i].SetActive(false); 
        }
    }

    public void UnlockPortal(int portalIndex) {
        StartCoroutine(UnlockPortalCoroutine(portalIndex));
    }

    public void UnlockFirstWorld() {
        gameStats.completedLevels[0] = true;
        UnlockPortal(0);
    }

    IEnumerator UnlockPortalCoroutine(int portalIndex) {
        yield return new WaitForSeconds(waitTime);
        portals[portalIndex].SetActive(true);
    }
}
