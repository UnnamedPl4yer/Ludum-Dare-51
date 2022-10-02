using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMeterController : MonoBehaviour
{
    public float maxTime;
    private float remainingTime;
    private bool timeRunning = false;

    [SerializeField] private Image gunStatusIcon;
    [SerializeField] private Sprite gunAvailableIcon;
    [SerializeField] private Sprite gunNotAvailableIcon;

    [SerializeField] private GameObject gunBar;
    private RectTransform gunBarTransform;
    private Vector2 originalSize;

    void Start() {
        gunBarTransform = gunBar.GetComponent<RectTransform>();
        originalSize = gunBarTransform.sizeDelta;
    }

    void Update() {
        UpdateIcon();
        if (!timeRunning) return;
        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Clamp(remainingTime, 0f, maxTime);
        ScaleBar();
    }

    void ScaleBar() {
        LeanTween.scaleY(gunBar, 1 - (remainingTime / maxTime), Time.deltaTime);
    }

    public void StartTimer(float givenMaxTime) {
        maxTime = givenMaxTime;
        remainingTime = givenMaxTime;
        timeRunning = true;
    }

    void UpdateIcon() {
        if (timeRunning) {
            gunStatusIcon.sprite = gunNotAvailableIcon;
            return;
        }
        gunStatusIcon.sprite = gunAvailableIcon;
    }
}
