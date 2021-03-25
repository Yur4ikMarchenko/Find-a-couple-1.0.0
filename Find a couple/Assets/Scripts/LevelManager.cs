using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Level
{
    public int numberOfPairs { get; }
    public int timeLimit { get; }
    public int tries { get; }
    public bool available { get; set; }

    public Level(int n,int tl,int t)
    {
        numberOfPairs = n;
        timeLimit = tl;
        tries = t;
        available = false;
    }
}

public class LevelManager : MonoBehaviour
{
    static Level[] levels = new Level[] { new Level(2,10,1),
                                   new Level(4,15,2),
                                   new Level(5,20,3),
                                   new Level(6,25,5),
                                   new Level(7,30,7),
                                   new Level(8,35,9),
                                   new Level(9,40,10),
                                   new Level(10,45,11),
                                   new Level(11,55,12),
                                   new Level(12,65,14)};

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
        if(!IsLastLevel())
        {
            levels[currentLevel + 1].available = true;
            Debug.Log("lvl" + (currentLevel + 1).ToString() + levels[currentLevel + 1].available.ToString());
        }
        SaveLevelProgress();
    }

    public static bool IsLevelAvailable(int index)
    {
        if (index == 0)
            return true;
        return levels[index].available;
    }
    public static bool IsLastLevel()
    {
        return currentLevel == levels.Length - 1;
    }

    public static void LoadLevelProgress()
    {
        int lvl = 0;
        if (PlayerPrefs.HasKey("LastOpenLevel") && PlayerPrefs.GetInt("LastOpenLevel") > 0)
            lvl = PlayerPrefs.GetInt("LastOpenLevel");
        for (int i = 0; i < levels.Length; ++i)
        {
            Debug.Log("lvl" + i.ToString()+" is "+(i<=lvl).ToString());
            levels[i].available = i <= lvl;
        }
    }

    public static void SaveLevelProgress()
    {
        for(int i = 0; i < levels.Length;++i)
            if(levels[i].available)
                PlayerPrefs.SetInt("LastOpenLevel", i);
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LastOpenLevel");
        LoadLevelProgress();
    }
}
