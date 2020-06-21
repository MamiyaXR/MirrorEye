using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectButton(transform, "ContinueButton");
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectButton(transform, "ContinueButton");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.PausePanel);
    }
    public void OnContinueButtonClick()
    {
        GameManager.Instance.ContinueGame();
    }
    public void OnQuitButtonClick()
    {
        GameManager.Instance.Save();
        UIManager.Instance.ClearPanel();
        GameManager.Instance.ContinueGame();
        GameManager.Instance.ReturnMenu();
    }
}
