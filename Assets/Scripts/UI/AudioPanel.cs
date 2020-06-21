using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AudioPanel : BasePanel
{
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider seSlider;
    protected override void Awake()
    {
        Init();
        base.Awake();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectSlider("Master Slider");
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectSlider("Master Slider");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.AudioPanel);
    }
    private void Init()
    {
        masterSlider.value = (float)GameManager.Instance.GameData.masterVolume;
        bgmSlider.value = (float)GameManager.Instance.GameData.bgmVolume;
        seSlider.value = (float)GameManager.Instance.GameData.seVolume;
    }
    public void OnMasterSlider()
    {
        GameManager.Instance.GameData.masterVolume = masterSlider.value;
        AudioManager.Instance.FixedMasterVolume();
    }
    public void OnBgmSlider()
    {
        GameManager.Instance.GameData.bgmVolume = bgmSlider.value;
        AudioManager.Instance.FixedBgmVolume();
    }
    public void OnSeSlider()
    {
        GameManager.Instance.GameData.seVolume = seSlider.value;
        AudioManager.Instance.FixedSeVolume();
    }
    public override void OnPopPanel()
    {
        GameManager.Instance.Save();
        base.OnPopPanel();
    }
}
