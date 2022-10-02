using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarDisplay : MonoBehaviour
{
    [SerializeField] private Transform compassNeedle;

    [SerializeField] private Transform monsterTransform;
    [SerializeField] private Transform playerTransform;

    void Update() {
        Vector3 monsterDirection = (monsterTransform.position - playerTransform.position);
        compassNeedle.rotation = Quaternion.LookRotation(monsterDirection.normalized) 
            * Quaternion.FromToRotation(Vector3.up, Vector3.forward) 
            * Quaternion.Euler(0, 90, 0);;
    }
}
