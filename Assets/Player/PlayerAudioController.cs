using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip stepSound;

    public GameSettings settings;

    private bool delayedPlaying = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStepSound() {
        if (audioSource.isPlaying || delayedPlaying) return;
        StartCoroutine(PlayWithPauseAfter(stepSound));
    }

    IEnumerator PlayWithPauseAfter(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.volume = settings.sfxVolume;
        delayedPlaying = true;
        audioSource.Play();
        yield return new WaitForSeconds(0.25f);
        delayedPlaying = false;
    }
}
