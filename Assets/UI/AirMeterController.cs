using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirMeterController : MonoBehaviour
{
    public float maxAir;
    public float remainingAir;
    public int chokingThreshold;

    [SerializeField] private Image airIcon;
    [SerializeField] private Sprite airFullIcon;
    [SerializeField] private Sprite airEmptyIcon;

    [SerializeField] private GameObject airBar;
    private RectTransform airBarTransform;
    private Vector2 originalSize;

    void Start() {
        airBarTransform = airBar.GetComponent<RectTransform>();
        originalSize = airBarTransform.sizeDelta;
    }

    void Update() {
        ScaleBar();
        UpdateIcon();
    }

    void ScaleBar() {
        LeanTween.scaleY(airBar, (remainingAir / maxAir), Time.deltaTime);
    }

    void UpdateIcon() {
        if (remainingAir > maxAir / (maxAir / chokingThreshold)) {
            airIcon.sprite = airFullIcon;
            return;
        }
        airIcon.sprite = airEmptyIcon;
    }
}
