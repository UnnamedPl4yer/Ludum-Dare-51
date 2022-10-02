using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject {

    public string speaker;
    [TextArea(0, 5)]
    public string[] lines;

    public float typingSpeed; //characters per second
    public AudioClip typingSound;

}
