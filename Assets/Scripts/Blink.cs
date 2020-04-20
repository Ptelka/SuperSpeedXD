using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour{
    private float timer;
    public float blinkTime = 0.1f;
    private bool visible = true;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > blinkTime)
        {
            visible = !visible;
            text.enabled = visible;
            timer = 0;
        }
    }
}