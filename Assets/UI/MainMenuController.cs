using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameSettings settings;

    public float musicVolume;
    public Slider musicVolumeSlider;
    public float sfxVolume;
    public Slider sfxVolumeSlider;
    public int difficulty;
    public TMP_Dropdown difficultySelect;

    public void StartGame() {
        SceneManager.LoadScene("TutorialScene");
    }

    public void UpdateSettings() {
        // Debug.Log("Music: " + musicVolume + ", SFX: " + sfxVolume + ", Difficulty: " + difficulty);
        settings.musicVolume = musicVolume;
        settings.sfxVolume = sfxVolume;
        settings.difficulty = difficulty;
    }

    public void SetMusicVolume() {
        // Debug.Log(musicVolumeSlider.value);
        musicVolume = (float)System.Math.Round(musicVolumeSlider.value, 2);
    }

    public void SetSFXVolume() {
        // Debug.Log(sfxVolumeSlider.value);
        sfxVolume = (float)System.Math.Round(sfxVolumeSlider.value, 2);
    }

    public void SetDifficulty() {
        // Debug.Log(difficultySelect.value);
        difficulty = difficultySelect.value;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
