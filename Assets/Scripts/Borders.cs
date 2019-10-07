using UnityEngine;

public class Borders : MonoBehaviour
{
    public Camera theCamera;
    public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D left;
    public BoxCollider2D right;

    void OnEnable()
    {
        RepositionAll();
    }

    public void RepositionAll()
    {
        Vector3 v = theCamera.ViewportToWorldPoint(new Vector2(1, 1));

        top.size = new Vector2(v.x * 2f, 1f);
        bottom.size = new Vector2(v.x * 2f, 1f);
        left.size = new Vector2(1f, v.y * 2f);
        right.size = new Vector2(1f, v.y * 2f);

        top.offset = new Vector2(0f, v.y + 0.5f);
        bottom.offset = new Vector2(0f, -v.y - 0.5f);
        left.offset = new Vector2(-(v.x + 0.5f), 0);
        right.offset = new Vector2(v.x + 0.5f, 0);
    }
}
