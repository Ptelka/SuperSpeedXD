using UnityEngine;

public class Parallax : MonoBehaviour {
    public GameObject[] backgrounds;
    public float multiplayer = 0.7f;

    private Vector2[] positions;

    public void Start()
    {
        positions = new Vector2[backgrounds.Length];
        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = backgrounds[i].transform.position;
        }
    }

    public void Update()
    {
        float current = multiplayer;

        for (int i = 0; i < backgrounds.Length; ++i)
        {
            var pos = positions[i];
            pos.x = pos.x + Curvature.Get() * current;
            backgrounds[i].transform.position = pos;
            current *= current;
        }
    }
}
