using System;
using Config;
using UnityEngine;

public class Road : MonoBehaviour {
    [SerializeField] private Vector2 size = new Vector2(0.8f, 0.6f);
    [SerializeField] private Color[] layers = {Color.gray, Color.black, Color.red};
    [SerializeField] private float[] layers_sizes = {0.4f, 0.05f, 0.05f};
    [SerializeField] private Color[] background = {Color.white, Color.yellow};
    [SerializeField] private float markings_size = 0.01f;
    
    private Material material;
    
    private static readonly int CurvatureId = Shader.PropertyToID("curvature");
    private static readonly int DistanceId = Shader.PropertyToID("distance");

    private Track track;
    private Track.Section section;

    private void Awake()
    {
        RoadSize.Set(size);
    }

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetVector("road_size", new Vector4(size.x, size.y));
        material.SetColorArray("layers", layers);
        material.SetInt("layers_count", layers.Length);
        material.SetColorArray("background", background);
        material.SetFloatArray("layers_sizes", layers_sizes);
        material.SetFloat("markings_size", markings_size);

        track = Config.Config.GetInstance().GetCurrentLevel().track;
    }

    void Update()
    {
        float distance = Distance.Get();

        material.SetFloat(CurvatureId, Curvature.Get());
        material.SetFloat(DistanceId, distance);

        var newSection = track.FindSection(distance);
        if (newSection != section)
        {
            section = newSection;
            Curvature.Next(section.curvature);
        }

        Curvature.Update(Distance.Delta());
    }
}
