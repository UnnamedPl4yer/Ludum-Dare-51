using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Monster") 
            SceneManager.LoadScene("GameOverScene");
            // StartCoroutine(WaitGameOver());
    }

    IEnumerator WaitGameOver() {
        yield return new WaitForSeconds(0.5f);
    }
}
