using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level03Control : MonoBehaviour
{
    public GameObject key;
    void Update()
    {
        if(GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), LevelType.Level03)].hasKey)
        {
            if (GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.AudioPanel)])
                key.SetActive(true);
            else
                key.SetActive(false);
        }
    }
}
