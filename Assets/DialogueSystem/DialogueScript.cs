using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public GameObject dialogParent;
    public Dialogue dialogue;
    private int index;

    // Start is called before the first frame update
    void Start() {
        textElement.text = dialogue.speaker + ": ";
        StartDialogue();
        Debug.Log(dialogue.lines);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Next!");
            if (textElement.text == dialogue.speaker + ": " + dialogue.lines[index]) {
                NextLine();
                Debug.Log("Next Line!");
            } else {
                StopAllCoroutines();
                textElement.text = dialogue.speaker + ": " + dialogue.lines[index];
                Debug.Log("Fill Line!");
            }
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (index < dialogue.lines.Length - 1) {
            index++;
            textElement.text = dialogue.speaker + ": ";
            StartCoroutine(TypeLine());
        } else {
            dialogParent.SetActive(false);
        }
    }

    IEnumerator TypeLine() {
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
