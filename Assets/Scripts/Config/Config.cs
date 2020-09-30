using System;
using System.IO;
using UnityEngine;

namespace Config {
    
public class Config {
    private static readonly Config instance = new Config();
    private Levels levels;
    private Levels.Level current;

    public static Config GetInstance()
    {
        return instance;
    }

    private Config()
    {
        levels = JsonUtility.FromJson<Levels>(Load("Assets/Config/Levels.json"));
    }

    string Load(string path)
    {
        return File.ReadAllText(path);
    }

    public Levels GetLevels()
    {
        return levels;
    }

    public void SetCurrentLevel(Levels.Level level)
    {
        current = level;
    }
    
    public Levels.Level GetCurrentLevel()
    {
        if (current == null)
        {
            throw new Exception("Current level is null");
        }
        return current;
    }

}

}
