using TMPro;
using UnityEngine;

public class FPSCalculator2 : MonoBehaviour
{
    public float updateInterval = 0.1f;
    public double alpha = 0.01;

    public TMP_Text text;

    public double averageDeltaTime;
    public double averageFPS;

    public float lastUpdateTime;

    void Start()
    {
        averageDeltaTime = 0;
        averageFPS = 0;

        lastUpdateTime = 0;
    }

    void Update()
    {
        averageDeltaTime = averageDeltaTime * (1 - alpha) + Time.unscaledDeltaTime * alpha;

        if (Time.unscaledTime > lastUpdateTime + updateInterval)
        {
            averageFPS = 1.0 / averageDeltaTime;
            text.text = "FPS: " + averageFPS.ToString("f2") + "\nDelta time: " + (averageDeltaTime * 1000.0).ToString("f2");
            lastUpdateTime = Time.unscaledTime;
        }
    }
}
