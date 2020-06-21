using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level02Control : MonoBehaviour
{
    public GameObject telescopObj;
    public GameObject overdata;
    private void Awake()
    {
        if (GameManager.Instance.GameData.hasTelescope)
            Destroy(telescopObj);
        if(GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), (LevelType)GameManager.Instance.GameData.stageCurrent)].hasKey)
            if (GameManager.Instance.lsProgress < 1.1f)
                Destroy(overdata);
    }
}
