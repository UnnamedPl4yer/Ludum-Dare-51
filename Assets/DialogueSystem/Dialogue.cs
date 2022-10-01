using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject {

    public string speaker;
    public string[] lines;

    public float typingSpeed; //characters per second
    public AudioSource typingSound;

}
