using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraSpeed = 2.0f;

    // Update is called once per frame
    void Update() {
        Vector3 position = transform.position;
        position.x = Mathf.Lerp(transform.position.x, playerTransform.position.x, cameraSpeed * Time.deltaTime);
        position.y = Mathf.Lerp(transform.position.y, playerTransform.position.y, cameraSpeed * Time.deltaTime);
        transform.position = position;
    }

    IEnumerator LerpToPlayerLocation(float durationInSeconds) {
        float time = 0;
        Vector2 startPosition = transform.position;
        while (time < durationInSeconds) {
            transform.position = Vector2.Lerp(startPosition, playerTransform.position, time / durationInSeconds);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = playerTransform.position;
    }
}
