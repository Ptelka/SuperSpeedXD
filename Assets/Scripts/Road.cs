using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
    public float curvature = 0f;
    public float perspectiveFactor = 0f;
    public Vector2 size = new Vector2(0.6f, 0.6f);
    public float distance;
    
    private Material material;
    
    private static readonly int Curvature = Shader.PropertyToID("_Curvature");
    private static readonly int Distance = Shader.PropertyToID("_Distance");

    public Vector2[] sections;

    private int section = 0;
    private float sum = 0;

    private Curvature cur = new Curvature();
    
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_RoadSizeY", size.y);
        material.SetFloat("_PerspectiveFactor", perspectiveFactor);
        material.SetFloat("_RoadSizeX", size.x / 2);

        foreach (var s in sections)
        {
            sum += s.x;
        }
    }

    void Update()
    {
        material.SetFloat(Curvature, curvature);
        material.SetFloat(Distance, distance);

        var newSection = FindSection();
        if (newSection != section)
        {
            section = newSection;
            cur.Next(sections[section].y);
        }

        curvature = cur.Get();
    }

    int FindSection()
    {
        float s = 0;
        float d = distance % sum;
        for (int i = 0; i < sections.Length; ++i)
        {
            s += sections[i].x;

            if (s > d)
            {
                return i;
            }
        }

        return 0;
    }
}
