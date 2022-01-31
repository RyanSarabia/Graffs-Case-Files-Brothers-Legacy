using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InstructionsData
{
    public bool[] instructLevel;

    public InstructionsData(int level, bool state)
    {
        instructLevel[level] = state;
    }
}
