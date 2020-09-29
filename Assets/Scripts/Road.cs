using System;
using UnityEngine;

public class Road : MonoBehaviour {
    public float curvature = 0f;
    public Vector2 size = new Vector2(0.8f, 0.6f);
    
    private float lastDistance;

    private Material material;
    
    private static readonly int CurvatureId = Shader.PropertyToID("curvature");
    private static readonly int DistanceId = Shader.PropertyToID("distance");

    public Vector2[] sections;

    private int section = 0;
    private float sum = 0;

    private void Awake()
    {
        RoadSize.Set(size);
    }

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetVector("road_size", new Vector4(size.x, size.y));
        material.SetColorArray("layers", new []{Color.gray, Color.black, Color.red});
        material.SetColorArray("background", new []{Color.white, Color.yellow});
        material.SetFloatArray("layers_sizes", new []{size.x/2f, 0.05f, 0.05f});
        material.SetInt("layers_count", 3);
        material.SetFloat("markings_size", 0.01f);

        foreach (var s in sections)
        {
            sum += s.x;
        }
    }

    void Update()
    {
        float distance = Distance.Get();
        float delta = distance - lastDistance;
        lastDistance = distance;
        
        material.SetFloat(CurvatureId, curvature);
        material.SetFloat(DistanceId, distance);

        var newSection = FindSection();
        if (newSection != section)
        {
            section = newSection;
            Curvature.Next(sections[section].y);
        }

        curvature = Curvature.Update(delta);
    }

    int FindSection()
    {
        float s = 0;
        float d = Distance.Get() % sum;
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
