using UnityEngine;

public static class Curvature {
    private static float start = 0;
    private static float next = 0;
    private static float progress = 0;
    private static float current = 0f;
    
    public static void Next(float c)
    {
        progress = 0;
        start = current;
        next = c;
    }

    public static float Get()
    {
        return current;
    }

    public static float Update(float delta = 0f)
    {
        progress += delta * Time.deltaTime;
        current = Mathf.Lerp(start, next, progress);
        return current;
    }
}