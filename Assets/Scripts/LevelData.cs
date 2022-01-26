using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int currentLevel;

    public LevelData(int level)
    {
        currentLevel = level;
    }
}
