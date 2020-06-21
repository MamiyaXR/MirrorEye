using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameScene : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public GameObject keyObj;
    private void Awake()
    {
        if(GameManager.Instance.playerDirect == Direct.Left)
            GameManager.Instance.SetPlayer(rightDoor.position, transform);
        else
            GameManager.Instance.SetPlayer(leftDoor.position, transform);
        GameManager.Instance.FixLight();
        GameManager.Instance.FixBrightness();
        if (!GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), (LevelType)GameManager.Instance.GameData.stageCurrent)].hasKey)
            Destroy(keyObj);
        string bgm = AudioManager.Instance.GetBgmNameByLevelType((LevelType)GameManager.Instance.GameData.stageCurrent);
        if (bgm != "")
            AudioManager.Instance.Play(bgm, true, 3f);
    }
    void Update()
    {
        if(Input.GetButtonDown("Option"))
        {
            GameManager.Instance.PauseGame();
        }
    }
}
