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
    static Level[] levels = new Level[] { new Level(2,10,2),
                                   new Level(4,15,4),
                                   new Level(5,20,5),
                                   new Level(6,25,6),
                                   new Level(7,30,8),
                                   new Level(8,35,10),
                                   new Level(9,40,12),
                                   new Level(10,45,14),
                                   new Level(11,55,14),
                                   new Level(12,65,18)};

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
        if(currentLevel<levels.Length-2)
            levels[currentLevel + 1].available = true;
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
            if(!levels[i].available)
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
