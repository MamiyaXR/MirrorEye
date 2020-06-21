using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectButton(transform, "Start Button");
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectButton(transform, "Start Button");
    }
    public void OnStartButtonClick()
    {
        UIManager.Instance.ClearPanel();
        GameManager.Instance.playerDirect = Direct.Right;
        GameManager.Instance.StartGame();
    }
    public void OnExitButtonClick()
    {
        GameManager.Instance.DestroyData();
        Application.Quit();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.MainPanel);
    }
}
