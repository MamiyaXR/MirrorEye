using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level01Control : MonoBehaviour
{
    public GameObject[] objs;
    public GameObject key;
    private void Awake()
    {
        if(!GameManager.Instance.GameData.isFirst)
        {
            for (int i = 0; i < objs.Length; i++)
                Destroy(objs[i]);
        }
        if(GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), (LevelType)GameManager.Instance.GameData.stageCurrent)].hasKey)
            if (GameManager.Instance.GameData.continousCount > -4)
                Destroy(key);
    }
}
