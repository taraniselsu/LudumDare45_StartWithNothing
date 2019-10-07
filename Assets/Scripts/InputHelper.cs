using UnityEngine;

public static class InputHelper
{
    public static bool Alt => Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
    public static bool Shift => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    public static bool Spawn => Input.GetMouseButtonDown(0) && !Shift;
    public static bool Feed => Input.GetMouseButtonDown(1) || (Input.GetMouseButtonDown(0) && Alt);
    public static bool Select => Input.GetMouseButtonDown(0) && Shift;
}
