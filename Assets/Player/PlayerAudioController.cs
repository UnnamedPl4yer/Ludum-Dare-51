using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip stepSound;
    public AudioClip pickUpSound;

    public GameSettings settings;

    private bool delayedPlaying = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.volume = settings.sfxVolume;
        audioSource.Play();
    }

    public void PlayStepSound() {
        if (audioSource.isPlaying || delayedPlaying) return;
        StartCoroutine(PlayWithPauseAfter(stepSound));
    }

    public void PlayPickUpSound() {
        //if (audioSource.isPlaying || delayedPlaying) return;
        PlaySound(pickUpSound);
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
