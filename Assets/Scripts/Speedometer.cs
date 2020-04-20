using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetSpeed(float speed)
    {
        text.text = "Speed: " + (int)speed;
    }
}
