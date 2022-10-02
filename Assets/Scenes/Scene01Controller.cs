using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Controller : MonoBehaviour
{
    public GameObject portal;
    public GameStats gameStats;
    public DialogueScript dialogueScript;
    public Dialogue portalOpenedDialogue;

    void Update() {
        foreach (bool collectible in gameStats.collectibles) {
            if (!collectible) return;
        }
        portal.SetActive(true);
        dialogueScript.StartDialogue(portalOpenedDialogue);
    }
}
