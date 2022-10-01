using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirMeterController : MonoBehaviour
{
    public float maxAir;
    public float remainingAir;

    [SerializeField] private GameObject airBar;
    private RectTransform airBarTransform;
    private Vector2 originalSize;

    void Start() {
        airBarTransform = airBar.GetComponent<RectTransform>();
        originalSize = airBarTransform.sizeDelta;
    }

    void Update() {
        ScaleBar();
    }

    void ScaleBar() {
        LeanTween.scaleY(airBar, (remainingAir / maxAir), Time.deltaTime);
    }
}
