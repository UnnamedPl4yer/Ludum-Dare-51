using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GamerOverSceneController : MonoBehaviour
{

    public TextMeshProUGUI diedText;
    private float timePassed = 0f;

    private AudioSource audioSource;

    public GameSettings settings;
    public AudioClip deathClip;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathClip;
        audioSource.volume = settings.sfxVolume;
        audioSource.Play();
        StartCoroutine(WaitRedirect());
    }

    void Update() {
        if (!audioSource.isPlaying) {
            audioSource.timeSamples = audioSource.clip.samples - 1;
            audioSource.pitch = -1;
            audioSource.Play();
        }
        diedText.color = Color.Lerp(Color.black, new Color(100, 0, 0, 1), timePassed / 3f);
        diedText.transform.localScale = Vector3.Slerp(new Vector3(0.7f, 0.7f, 1f), new Vector3(1f, 1f, 1f), timePassed / 3f);
        Vector3 lerpedRotation = Vector3.Slerp(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 5f), timePassed / 3f);
        diedText.transform.rotation = Quaternion.Euler(lerpedRotation);
        timePassed += Time.deltaTime;
    }

    IEnumerator WaitRedirect() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
