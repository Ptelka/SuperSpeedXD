using UnityEngine;

public class Curvature {
    private float start = 0;
    private float next = 0;
    private float timer = 0;
    private float current = 0f;
    
    public void Next(float c)
    {
        timer = 0;
        start = current;
        next = c;
    }

    public float Get()
    {
        timer += Time.deltaTime / 2;
        current = Mathf.Lerp(start, next, timer);
        return current;
    }
}