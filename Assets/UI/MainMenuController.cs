using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameSettings settings;
    public GameStats gameStats;

    public float musicVolume;
    public Slider musicVolumeSlider;

    public float sfxVolume;
    public Slider sfxVolumeSlider;

    public int difficulty;
    public TMP_Dropdown difficultySelect;

    private float fadeInTime = 2f;
    public Image greyOutImage;

    void Start() {
        greyOutImage.gameObject.SetActive(false);
    }

    public void StartGame() {
        // Reset progress
        for (int i = 0; i < gameStats.completedLevels.Length; i++) {
            gameStats.completedLevels[i] = false;
        }
        gameStats.lastOldManDialogue = -1;
        gameStats.nextOldManDialogue = 0;
        // End reset
        greyOutImage.gameObject.SetActive(true);
        StartCoroutine(FadeImageIn());        
    }

    IEnumerator FadeImageIn() {
        for (float i = 0f; i < 1f; i += (Time.deltaTime / fadeInTime)) {
            greyOutImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
        SceneManager.LoadScene("LoadingScene");
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
