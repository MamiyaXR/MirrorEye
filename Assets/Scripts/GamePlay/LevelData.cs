using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum LevelType : int
{
    Level01,
    Level02,
    Level03,
    Level04,
    Level05
}

[Serializable]
public class LevelData
{
    public string sceneName;
    public bool isClear;
    public bool hasKey;

    public LevelData() { }
    public LevelData(string sceneName, bool isClear, bool hasKey)
    {
        this.sceneName = sceneName;
        this.isClear = isClear;
        this.hasKey = hasKey;
    }
}
