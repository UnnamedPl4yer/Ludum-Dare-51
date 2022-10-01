using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMeterController : MonoBehaviour
{
    public float maxTime;
    private float remainingTime;
    private bool timeRunning = false;

    [SerializeField] private GameObject gunBar;
    private RectTransform gunBarTransform;
    private Vector2 originalSize;

    void Start() {
        gunBarTransform = gunBar.GetComponent<RectTransform>();
        originalSize = gunBarTransform.sizeDelta;
    }

    void Update() {
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
}
