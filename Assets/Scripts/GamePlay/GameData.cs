using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public static readonly string playerPath = "Player";
    public static readonly string lightPath = "Global Light";
    public static readonly string postProcessingPath = "Post Process Object";
    public static readonly int uiFlipDepth = -8;
    public static readonly int nFlipDepth = 0;


    public Dictionary<string, LevelData> levelDataDict;
    public Dictionary<string, bool> panelStateDict;
    public int stageCurrent;
    public double masterVolume;
    public double bgmVolume;
    public double seVolume;
    public double videoValue;

    public bool isFirst;
    public bool hasTelescope;
    public int keyNum;
    public int continousCount = 0;

    public GameData()
    {
        levelDataDict = new Dictionary<string, LevelData>();
        panelStateDict = new Dictionary<string, bool>();
    }
}
