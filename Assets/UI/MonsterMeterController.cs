using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMeterController : MonoBehaviour
{
    public float maxTime;
    private float remainingTime;
    private bool timeRunning = false;
    public bool isHunting = false;

    [SerializeField] private Image monsterStateIcon;
    [SerializeField] private Sprite monsterWatchingIcon;
    [SerializeField] private Sprite monsterWaitingIcon;

    [SerializeField] private GameObject timerBar;
    private RectTransform timerBarTransform;
    private Vector2 originalSize;

    void Start() {
        timerBarTransform = timerBar.GetComponent<RectTransform>();
        originalSize = timerBarTransform.sizeDelta;
    }

    void Update() {
        UpdateIcon();
        if (!timeRunning) return;
        
        if (remainingTime > 0.0f) {
            remainingTime -= Time.deltaTime;
            Vector3 oldPos = transform.position;
            LeanTween.scaleY(timerBar, (remainingTime / maxTime), Time.deltaTime);
            // LeanTween.move(timerBar, oldPos - new Vector3(0, (remainingTime / maxTime), 0), Time.deltaTime);
            return;
        }
        timeRunning = false;
        timerBarTransform.sizeDelta = originalSize;
    }

    public void StartTimer(float givenMaxTime) {
        maxTime = givenMaxTime;
        remainingTime = givenMaxTime;
        timeRunning = true;
    }

    void UpdateIcon() {
        if (timeRunning || isHunting) {
            monsterStateIcon.sprite = monsterWaitingIcon;
            return;
        }
        monsterStateIcon.sprite = monsterWatchingIcon;
    }
}
