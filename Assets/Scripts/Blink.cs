using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour{
    private float timer;
    public float blinkTime = 0.1f;
    private bool visible = true;

    private Text text;
    private new Renderer renderer;

    void Start()
    {
        text = GetComponent<Text>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > blinkTime)
        {
            visible = !visible;
            timer = 0;
            
            UpdateVisibility();
        }
    }

    void UpdateVisibility()
    {
        if (text)
            text.enabled = visible;
        else if (renderer)
            renderer.enabled = visible;
    }
}