using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{   
    [SerializeField] private Image greyOutImage;
    private float fadeTime = 1f;

    void Start() {
        greyOutImage.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeImageOut());
    }

    IEnumerator FadeImageOut() {
        yield return new WaitForSeconds(1f);
        for (float i = 1f; i < 0f; i -= (Time.deltaTime / fadeTime)) {
            greyOutImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
        StartCoroutine(FadeImageIn());
    }

    IEnumerator FadeImageIn() {
        for (float i = 0f; i < 1f; i += (Time.deltaTime / fadeTime)) {
            greyOutImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
        SceneManager.LoadScene("HubScene");
    }
}
