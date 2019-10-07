using UnityEngine;

public class Feed : MonoBehaviour
{
    public GameObject feedPrefab;
    public Camera theCamera;

    void Update()
    {
        if (InputHelper.Feed)
        {
            var pos = theCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            Instantiate(feedPrefab, pos, Quaternion.identity);
        }
    }
}
