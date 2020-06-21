using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class StagePanel : BasePanel
{
    public GameObject stageButtonGroup;
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectButton(transform, "Stage 01");
        InitStageButtonGroup();
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectButton(transform, "Stage 01");
        InitStageButtonGroup();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.StagePanel);
    }
    public void OnStageButtonClick(int level)
    {
        UIManager.Instance.ClearPanel();
        GameManager.Instance.StartGame((LevelType)level);
    }
    private void InitStageButtonGroup()
    {
        for(int i = 1; i < stageButtonGroup.transform.childCount; i++)
        {
            if (GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), i - 1)].isClear)
                stageButtonGroup.transform.GetChild(i).gameObject.SetActive(true);
            else
                stageButtonGroup.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
