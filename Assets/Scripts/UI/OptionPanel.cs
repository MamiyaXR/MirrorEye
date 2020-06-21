using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionPanel : BasePanel
{
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectButton(transform, "Audio Button");
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectButton(transform, "Audio Button");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.OptionPanel);
    }
}
