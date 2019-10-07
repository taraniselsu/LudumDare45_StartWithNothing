using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public TMP_Text numOrganismsLabel;
    public TMP_Text numOrganismsValue;
    public TMP_Text generationLabel;
    public TMP_Text generationValue;
    public TMP_Text timeLabel;
    public TMP_Text timeValue;
    public TMP_Text fpsLabel;
    public FPSCalculator2 fpsCalculator;
    public Main main;

    void Update()
    {
        if (main.count > 100)
        {
            if (!numOrganismsLabel.gameObject.activeSelf)
            {
                numOrganismsLabel.gameObject.SetActive(true);
                numOrganismsValue.gameObject.SetActive(true);
            }

            string countStr = main.count.ToString();
            if (main.maxNumCellsCounter.Count > 1 && main.maxNumCellsCounter[1] >= 10)
            {
                countStr += " (" + string.Join(", ", main.maxNumCellsCounter) + ")";
            }
            numOrganismsValue.text = countStr;
        }

        if (main.maxGeneration >= 10)
        {
            if (!generationLabel.gameObject.activeSelf)
            {
                generationLabel.gameObject.SetActive(true);
                generationValue.gameObject.SetActive(true);
            }
            generationValue.text = main.maxGeneration.ToString();
        }

        if (Time.timeSinceLevelLoad > 300)
        {
            if (!timeLabel.gameObject.activeSelf)
            {
                timeLabel.gameObject.SetActive(true);
                timeValue.gameObject.SetActive(true);
            }
            timeValue.text = Time.time.ToString("f0");
        }

        if (Time.timeSinceLevelLoad > 10f && fpsCalculator.averageFPS < 30f && !fpsLabel.gameObject.activeSelf)
        {
            fpsLabel.gameObject.SetActive(true);
        }
    }
}
