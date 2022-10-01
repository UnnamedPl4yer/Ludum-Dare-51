using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarDisplay : MonoBehaviour
{
    [SerializeField] private GameObject sonarDot;

    [SerializeField] private Transform monsterTransform;
    [SerializeField] private Transform playerTransform;

    private Vector2 sonarSize;

    void Start() {
        sonarSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    void Update() {
        Vector3 distance = (monsterTransform.position - playerTransform.position);
        Vector3 direction = distance.normalized;
        sonarDot.transform.position = transform.position + (ClampVector(distance, -20.0f, 20.0f) / 20) * sonarSize.x / 2;
        // Debug.Log(sonarDot.transform.localScale);
        float scale = Clamp(20 / distance.magnitude, 1.0f, 2.0f);
        sonarDot.transform.localScale = new Vector3(
            scale,
            scale,
            1.0f
        );
        // Debug.Log(direction);
    }

    float Clamp(float value, float min, float max) {
        return value < min ? min : value > max ? max : value;
    }

    Vector3 ClampVector(Vector3 vec, float min, float max) {
        if (vec.x < min) vec.x = min;
        if (vec.x > max) vec.x = max;
        if (vec.y < min) vec.y = min;
        if (vec.y > max) vec.y = max;
        if (vec.z < min) vec.z = min;
        if (vec.z > max) vec.z = max;
        return vec;
    }
}
