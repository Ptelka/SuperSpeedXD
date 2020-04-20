using UnityEngine;

public class Line {
    private GameObject left;
    private GameObject right;

    private Vector2 lscale;
    private Vector2 rscale;

    private float perspectiveFactor;
    private float perspective;
    private float position;
    private float roadHeight;
    
    public Line(float roadHeight, float position, GameObject left, GameObject right, float perspectiveFactor)
    {
        this.position = position;
        this.roadHeight = roadHeight;
        this.perspective = Perspective(roadHeight);
        this.left = left;
        this.right = right;
        this.perspectiveFactor = perspectiveFactor;
        
        if (left)
        {
            lscale = left.transform.localScale;
        }
        if (right)
        {
            rscale = right.transform.localScale;
        }
    }
    
    float Perspective(float roadHeight)
    {
        return position / (roadHeight * perspectiveFactor);
    }

    public void SetVisible(bool visible)
    {
        if (left)
        {
            left.SetActive(visible);
        }
        if (right)
        {
            right.SetActive(visible);
        }
    }

    public void Update(float curvature)
    {
        this.perspective = Perspective(roadHeight);

        float roadWidth = 1.0f - perspective;
        float middlePoint = 0.5f + curvature * Mathf.Pow(perspective, 3.0f);

        float offsetX = 0.7f * roadWidth;
        
        if (left)
        {
            left.transform.position = Screen2World(new Vector2(middlePoint - offsetX, position));
            left.transform.localScale = lscale * roadWidth;
        }
        if (right)
        {
            right.transform.position = Screen2World(new Vector2(middlePoint + offsetX, position));
            right.transform.localScale = rscale * roadWidth;
        }
    }

    public float GetPerspective()
    {
        return perspective;
    }
    
    Vector2 Screen2World(Vector2 vec)
    {
        vec.x = Camera.main.pixelWidth * vec.x;
        vec.y = Camera.main.pixelHeight * vec.y;

        return Camera.main.ScreenToWorldPoint(vec);
    }
}