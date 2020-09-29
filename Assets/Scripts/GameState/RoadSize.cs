using UnityEngine;

public static class RoadSize {
    private static Vector2 size;
    
    public static Vector2 Get()
    {
        return size;
    }

    public static void Set(Vector2 s)
    {
        size = s;
    }
}