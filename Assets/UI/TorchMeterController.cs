using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchMeterController : MonoBehaviour
{
    public float maxTime;
    private float remainingTime;
    private bool timeRunning = false;

    [SerializeField] private Image torchStatusIcon;
    [SerializeField] private Sprite[] torchLevelIcons;
    public int index;

    [SerializeField] private GameObject timerBar;
    private RectTransform timerBarTransform;
    private Vector2 originalSize;

    public float rechargeProgress;
    public Image rechargeProgressIndicator;

    void Start() {
        timerBarTransform = timerBar.GetComponent<RectTransform>();
        originalSize = timerBarTransform.sizeDelta;
    }

    void Update() {
        rechargeProgressIndicator.fillAmount = rechargeProgress;
        UpdateIcon();
        if (!timeRunning) return;

        if (remainingTime > 0.0f) {
            remainingTime -= Time.deltaTime;
            LeanTween.scaleY(timerBar, (remainingTime / maxTime), Time.deltaTime);
            return;
        }
        timeRunning = false;
        timerBarTransform.sizeDelta = originalSize;
    }

    public void StartTimer(float torchTime) {
        maxTime = torchTime;
        remainingTime = torchTime;
        timeRunning = true;
    }

    void UpdateIcon() {
        torchStatusIcon.sprite = torchLevelIcons[index];
    }
}
