using UnityEngine;

public class Parallax {
    private GameObject[] backgrounds;
    private Vector2[] positions;

    public Parallax(GameObject[] backgrounds)
    {
        positions = new Vector2[backgrounds.Length];
        for (int i = 0; i < positions.Length; ++i)
        {
            positions[i] = backgrounds[i].transform.position;
        }

        this.backgrounds = backgrounds;
    }


    public void Update(float curvature)
    {
        float multiplayer = 0.7f;

        for (int i = 0; i < backgrounds.Length; ++i)
        {
            var pos = positions[i];
            pos.x = pos.x + curvature * multiplayer;
            backgrounds[i].transform.position = pos;
            multiplayer *= multiplayer;
        }
    }
}
