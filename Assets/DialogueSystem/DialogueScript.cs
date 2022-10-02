using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI speakerElement;
    public TextMeshProUGUI textElement;
    [SerializeField] private GameObject dialogueParent;
    private Dialogue dialogue;
    private int index;

    // Start is called before the first frame update
    void Start() {
        dialogueParent.SetActive(false);
        // speakerElement.text = string.Empty;
        // textElement.text = string.Empty;
        // Debug.Log(dialogue.lines);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textElement.text == dialogue.lines[index].Replace("#", string.Empty)) {
                NextLine();
            } else {
                StopAllCoroutines();
                textElement.text = dialogue.lines[index].Replace("#", string.Empty);
            }
        }
    }

    public void StartDialogue(Dialogue dialogueToSpeak) {
        dialogue = dialogueToSpeak;
        index = 0;
        dialogueParent.SetActive(true);
        speakerElement.text = string.Empty;
        textElement.text = string.Empty;

        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (index < dialogue.lines.Length - 1) {
            index++;
            speakerElement.text = dialogue.speaker;
            textElement.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            dialogueParent.SetActive(false);
        }
    }

    IEnumerator TypeLine() {
        speakerElement.text = dialogue.speaker;
        foreach (char c in dialogue.lines[index].ToCharArray()) {
            if (c == '#') {
                yield return new WaitForSeconds(2.0f);
                continue;
            }
            textElement.text += c;
            yield return new WaitForSeconds(1 / dialogue.typingSpeed);
        }
    }
}
