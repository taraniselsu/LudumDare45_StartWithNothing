using UnityEngine;
using UnityEngine.UI;

public class FPSCalculator : MonoBehaviour
{
    public float updateInterval = 0.1f;
    public double alpha = 0.01;

    public Text text;

    public double averageFPS;
    public double averageDeltaTime;

    public float lastUpdateTime;
    public int lastUpdateFrames;

    void Start()
    {
        text = GetComponent<Text>();

        averageFPS = 60;
        averageDeltaTime = 0;

        lastUpdateTime = 0;
        lastUpdateFrames = 0;
    }

    void Update()
    {
        if (Time.unscaledTime > lastUpdateTime + updateInterval)
        {
            float deltaTime = Time.unscaledTime - lastUpdateTime;
            int deltaFrames = Time.frameCount - lastUpdateFrames;

            averageFPS = averageFPS * (1 - alpha) + (deltaFrames / deltaTime) * alpha;

            // ??? Just to match the info available in the other FPS calculator
            averageDeltaTime = 1.0 / averageFPS;

            text.text = "FPS: " + averageFPS.ToString("f2") + "\nDelta time: " + (averageDeltaTime * 1000.0).ToString("f2");

            lastUpdateTime = Time.unscaledTime;
            lastUpdateFrames = Time.frameCount;
        }
    }
}
