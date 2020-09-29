using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowOnVelocity : MonoBehaviour {
    public float min = -1;
    public float max = 20;
    
    private Renderer rend;
    private Text text;
    
    
    private void Start()
    {
        rend = GetComponent<Renderer>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        var velocity = Velocity.Get();
        var visible = velocity < max && velocity > min;
        if (rend)
        {
            rend.enabled = visible;
        }

        if (text)
        {
            text.enabled = visible;
        }
    }
}
