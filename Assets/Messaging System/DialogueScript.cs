using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public string[] lines;
    public int charactersPerSecond;
    private int index;

    // Start is called before the first frame update
    void Start() {
        textElement.text = string.Empty;
        StartDialogue();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textElement.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textElement.text = lines[index];
            }
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textElement.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            textElement.text += c;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }
    }
}
