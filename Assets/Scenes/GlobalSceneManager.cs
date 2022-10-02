using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public void LoadHubScene() {
        Debug.Log("Loading Hub");
        SceneManager.LoadScene("HubScene");
    }

    public void LoadSceneByName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }    
}
