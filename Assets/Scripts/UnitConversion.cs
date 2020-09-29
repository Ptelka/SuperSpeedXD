using UnityEngine;

public static class UnitConversion {
    private static Camera camera;
    
    public static void SetCamera(Camera cam)
    {
        camera = cam;
    }
    
    public static Vector2 PointToScreenRatio(Vector2 vec)
    {
        vec = camera.WorldToScreenPoint(vec);
        vec.x =  vec.x / camera.pixelWidth;
        vec.y = vec.y / camera.pixelHeight;

        return vec;
    }

    public static Vector2 ScreenRatioToPoint(Vector2 vec)
    {
        vec.x = camera.pixelWidth * vec.x;
        vec.y = camera.pixelHeight * vec.y;

        return camera.ScreenToWorldPoint(vec);
    }
}
