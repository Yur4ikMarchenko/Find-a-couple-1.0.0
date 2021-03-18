using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Level
{
    public int numberOfPairs { get; }
    public int timeLimit { get; }
    public int tries { get; }
    public bool clear { get; set; }

    public Level(int n,int tl,int t)
    {
        numberOfPairs = n;
        timeLimit = tl;
        tries = t;
        clear = false;
    }
}

public class LevelManager : MonoBehaviour
{
    static Level[] levels = new Level[] { new Level(2,15,2),
                                   new Level(4,15,3),
                                   new Level(5,20,4),
                                   new Level(6,30,6),
                                   new Level(7,30,8),
                                   new Level(8,30,10),
                                   new Level(9,35,12),
                                   new Level(10,40,14),
                                   new Level(11,45,14),
                                   new Level(12,50,16)};

    public static int currentLevel { get; private set; }

    public static int pairs;
    public static int limit;
    public static int tries;

    public static void SetLevel(int index)
    {
        pairs = levels[index].numberOfPairs;
        limit = levels[index].timeLimit;
        tries = levels[index].tries;
        currentLevel = index;
    }

    public static void Clear()
    {
        levels[currentLevel].clear = true;
        SaveLevelProgress();
    }

    public static bool IsLevelAvailable(int index)
    {
        if (index == 0)
            return true;
        return levels[index - 1].clear;
    }
    public static bool IsLastLevel()
    {
        return currentLevel == levels.Length - 1;
    }

    public static void LoadLevelProgress()
    {
        int lvl = 0;
        if (PlayerPrefs.HasKey("LastOpenLevel"))
            lvl = PlayerPrefs.GetInt("LastOpenLevel");
        for (int i = 0; i < levels.Length; ++i)
        {
            levels[i].clear = i <= lvl;
        }

    }

    public static void SaveLevelProgress()
    {
        for(int i = 0; i < levels.Length;++i)
            if(!levels[i].clear)
            {
                PlayerPrefs.SetInt("LastOpenLevel", i - 1);
                break;
            }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LastOpenLevel");
        LoadLevelProgress();
    }
}
