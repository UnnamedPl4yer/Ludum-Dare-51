using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class Flicker2 : MonoBehaviour
{
    public new Light2D light;
    public Light2D outerLight;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public int currentLevel = 0;
    public float[] intensitySteps = {1f, 0.67f, 0.33f, 0.0f};
    private bool torchDecrease = false;
    public float torchLevelFluctuations = .1f;
    public float timeHeldToReignite = 0f;
    public float timeNeededToReignite = 2f;
    [Range(1, 50)]
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;

    public TorchMeterController torchMeterController;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light2D>();
        }
        StartCoroutine(WaitUntilNextStage());
    }

    void Update()
    {
        if (light == null)
            return;

        if (Input.GetMouseButton(1)) { // Hold right mouse button to reignite
            timeHeldToReignite += Time.deltaTime;
            torchMeterController.rechargeProgress = timeHeldToReignite / timeNeededToReignite;
            if (timeHeldToReignite >= timeNeededToReignite) {
                currentLevel -= 1;
                currentLevel = Mathf.Clamp(currentLevel, 0, intensitySteps.Length-1);
                timeHeldToReignite = 0f;
                torchMeterController.StartTimer(10f);
            }
        } else {
            timeHeldToReignite = 0f;
            torchMeterController.rechargeProgress = 0f;
        }
        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(intensitySteps[currentLevel] * (1 - torchLevelFluctuations), intensitySteps[currentLevel] * (1 + torchLevelFluctuations));
        // float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
        outerLight.intensity = lastSum / (float)smoothQueue.Count;
    }

    IEnumerator WaitUntilNextStage() {
        torchMeterController.StartTimer(10f);
        yield return new WaitForSeconds(10);
        torchDecrease = true;
        currentLevel += 1;
        currentLevel = Mathf.Clamp(currentLevel, 0, intensitySteps.Length-1);
        StartCoroutine(WaitUntilNextStage());
    }
}