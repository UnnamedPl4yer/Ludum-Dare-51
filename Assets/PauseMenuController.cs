using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public GameSettings settings;

    public GameObject pauseMenuParent;
    public GameObject dialogueBox;
    public DialogueScript dialogueController;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private float musicVolume;
    private float sfxVolume;
    private bool menuActive = false;

    void Start() {
        pauseMenuParent.SetActive(menuActive);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            menuActive = !menuActive;
            pauseMenuParent.SetActive(menuActive);
            dialogueBox.SetActive(!menuActive);
            dialogueController.enabled = !menuActive;
            Time.timeScale = menuActive ? 0 : 1; // Stop physics time
        }
    }

    public void UpdateSettings() {
        // Debug.Log("Music: " + musicVolume + ", SFX: " + sfxVolume + ", Difficulty: " + difficulty);
        settings.musicVolume = musicVolume;
        settings.sfxVolume = sfxVolume;
    }

    public void SetMusicVolume() {
        // Debug.Log(musicVolumeSlider.value);
        musicVolume = (float)System.Math.Round(musicVolumeSlider.value, 2);
    }

    public void SetSFXVolume() {
        // Debug.Log(sfxVolumeSlider.value);
        sfxVolume = (float)System.Math.Round(sfxVolumeSlider.value, 2);
    }

    public void ExitMenu() {
        menuActive = false;
        pauseMenuParent.SetActive(false);
        dialogueBox.SetActive(true);
        dialogueController.enabled = true;
        Time.timeScale = 1;
    }

    public void BackToMainMenu() {
        ExitMenu();
        SceneManager.LoadScene("MainMenu");
    }
}
