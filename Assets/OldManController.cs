using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManController : MonoBehaviour
{
    [SerializeField] GameStats gameStats;
    [SerializeField] Dialogue[] dialogues;
    private DialogueScript dialogueScript;
    private bool playerWantsToSpeak = false;
    private bool isSpeaking = false;
    private HubManager hubManager;


    void Start() {
        // Debug! Take out before build!
        gameStats.lastOldManDialogue = -1;
        gameStats.nextOldManDialogue = 0;
        // End debug

        dialogueScript = GetComponent<DialogueScript>();
        hubManager = GetComponent<HubManager>();
        // Debug.Log((gameStats.lastOldManDialogue == gameStats.nextOldManDialogue));
        if (gameStats.lastOldManDialogue == gameStats.nextOldManDialogue) {
            return;
        }
        dialogueScript.StartDialogue(dialogues[gameStats.nextOldManDialogue]);
        gameStats.lastOldManDialogue = gameStats.nextOldManDialogue;
        gameStats.nextOldManDialogue += 1;
        gameStats.collectibles[0] = true;
    }

    void Update() {
        // if (playerWantsToSpeak && Input.GetKeyDown(KeyCode.F)) {
        //     SpeakNextDialogue();
        // }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag != "Player") return;
        playerWantsToSpeak = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag != "Player") return;
        playerWantsToSpeak = false;
    }

    public void SpeakNextDialogue() {
        dialogueScript.StartDialogue(dialogues[gameStats.nextOldManDialogue]);
        gameStats.lastOldManDialogue = gameStats.nextOldManDialogue;
        for (int i = 0; i < gameStats.collectibles.Length; i++) {
            if (gameStats.collectibles[i]) {
                hubManager.UnlockPortal(i);
            }
        }
    }
}