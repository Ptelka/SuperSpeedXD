using System;
using Config;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour {
    private Levels levels;
    private int id = -1;
    
    void Start()
    {
        UnitConversion.SetCamera(Camera.main);
        levels = Config.Config.GetInstance().GetLevels();
    }

    public void GameStarted()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        var current = id + 1;
        if (levels.levels.Count < current + 1)
        {
            throw new Exception("No such level: " + current);
        }

        id = current;
        var level = levels.levels[id];
        Config.Config.GetInstance().SetCurrentLevel(level);
        Load(level.scene);
    }

    public void MainMenu()
    {
        id = -1;
        Config.Config.GetInstance().SetCurrentLevel(null);
        Load("MainMenu");
    }

    private void Load(string scene)
    {
        Debug.Log("Starting " + scene);
        SceneManager.LoadScene("Scenes/" + scene);
    }
}