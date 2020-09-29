using UnityEngine;

public class RoadCommon {
    public static float PERSPECTIVE_FACTOR()
    {
        return 1.05f;
    }
    
    public static float PERSPECTIVE(float yPos, float height) {
        return yPos / (height * PERSPECTIVE_FACTOR());
    }

    public static float CURVE(float curvature, float perspective)
    {
        return curvature * Mathf.Pow(perspective, 3.0f);
    }
    
    public static float MOVE_ILLUSION_FUNCTION(float factor, float perspective, float distance) {
        return Mathf.Sin(factor * Mathf.Pow(perspective, 3.0f) + distance * 0.1f);
    }
}