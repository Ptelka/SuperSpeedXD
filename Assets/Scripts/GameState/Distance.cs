public static class Distance {
    private static float value = 0;
    private static float delta = 0;
    private static float old = 0;

    public static float Get()
    {
        return value;
    }
    
    public static float Delta()
    {
        return delta;
    }

    public static void Set(float v)
    {
        delta = v - old;
        value = old = v;
    }
}