using TMPro;
using UnityEngine;

public class Help : MonoBehaviour
{
    public bool hasSpawned = false;
    public bool hasFed = false;
    public bool hasSelected = false;
    public TMP_Text helpText;

    public float wait1 = 15f;
    public float wait2 = 60f;
    public float wait3 = 120f;

    void Update()
    {
        if (InputHelper.Spawn)
        {
            hasSpawned = true;
        }
        if (InputHelper.Feed)
        {
            hasFed = true;
        }
        if (InputHelper.Select)
        {
            hasSelected = true;
        }

        string message = "";
        if (!hasSpawned && Time.timeSinceLevelLoad > wait1)
        {
            message = "Have you tried left-clicking?";
        }
        else if (!hasFed && Time.timeSinceLevelLoad > wait2)
        {
            message = "Have you tried right-clicking? (or holding Alt while left-clicking?)";
        }
        else if (!hasSelected && Time.timeSinceLevelLoad > wait3)
        {
            //message = "Have you tried holding Shift while left-clicking?";
        }

        helpText.text = message;
    }
}
