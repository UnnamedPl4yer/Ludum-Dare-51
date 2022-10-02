using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStats : ScriptableObject
{
    public bool[] completedLevels;
    public bool[] collectibles;
    public int lastOldManDialogue;
    public int nextOldManDialogue;
}
