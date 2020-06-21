using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.PushPanel(UIPanelType.MainPanel);
        AudioManager.Instance.Play("menu_bgm", true);
        //UIManager.Instance.SetDefaultPopPanel(UIPanelType.MainPanel, GetComponentInChildren<BasePanel>());
    }
}
