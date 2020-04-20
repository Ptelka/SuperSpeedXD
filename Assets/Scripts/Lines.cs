using UnityEngine;

public class Lines {
    private Line[] lines;
    private float height;
    
    public Lines(GameObject left, GameObject right, float perspectiveFactor, float roadSize)
    {
        height = Camera.main.pixelHeight;
        lines = new Line[(int)(height * roadSize)];
        
        for (int i = 0; i < lines.Length; ++i)
        {
            var l = Object.Instantiate(left);
            var r = Object.Instantiate(right);

            l.GetComponent<SpriteRenderer>().sortingOrder = lines.Length - i;
            r.GetComponent<SpriteRenderer>().sortingOrder = lines.Length - i;
            lines[i] = new Line(0.6f, Normalize(i, height, 0), l, r, perspectiveFactor);
        }
    }

    public void Update(float curvature, float distance)
    {
        for (int i = 0; i < lines.Length; ++i)
        {
            lines[i].SetVisible(false);
            lines[i].Update(curvature);
        }
        
        SetVisible(distance);
    }
    
    void SetVisible(float distance)
    {
        for (int i = 0; i < lines.Length; ++i)
        {
            var sin = Mathf.Sin(20f * Mathf.Pow(lines[i].GetPerspective(), 3.0f) + distance * 0.1f);
            
            if (sin > 0.99f ||  sin < -0.99f || Mathf.Abs(sin) < 0.15f)
            {
                lines[i].SetVisible(true);
                if (i < lines.Length / 2)
                    i += 5;
            }
        }
        
    }
    
    float Normalize(float val, float max, float min)
    {
        return (val - min) / (max - min);
    }
}