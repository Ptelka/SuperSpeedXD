using System;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    private GameController controller;
    private Button button;
    void Start()
    {
        controller = FindObjectOfType<GameController>();

        if (controller == null)
        {
            throw new Exception("Couldn't find GameController on scene");
        }

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        controller.GameStarted();
    }
}
