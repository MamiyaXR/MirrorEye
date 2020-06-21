using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BgmFlipCallback : UIFlipCallback
{
    public override void OnFlipCallback()
    {
        if (!CanCallBack())
            return;

        UIManager.Instance.hasFlipedPanel = !UIManager.Instance.hasFlipedPanel;
        panel.isFilped = !panel.isFilped;

        GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.AudioPanel)] =
            !GameManager.Instance.GameData.panelStateDict[Enum.GetName(typeof(UIPanelType), UIPanelType.AudioPanel)];

        if(GameManager.Instance.GameState != GameState.InMenu)
        {
            string bgm = AudioManager.Instance.GetBgmNameByLevelType((LevelType)GameManager.Instance.GameData.stageCurrent);
            if (bgm != "")
            {
                AudioManager.Instance.StopAllAudio();
                AudioManager.Instance.Play(bgm, true);
            }
        }
    }
}
