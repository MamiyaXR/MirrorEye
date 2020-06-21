using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightFlipCallback : UIFlipCallback
{
    public override void OnFlipCallback()
    {
        if (!CanCallBack())
            return;

        UIManager.Instance.hasFlipedPanel = !UIManager.Instance.hasFlipedPanel;
        panel.isFilped = !panel.isFilped;

        GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.VideoPanel)] =
            !GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.VideoPanel)];

        GameManager.Instance.FixLight();
    }
}
