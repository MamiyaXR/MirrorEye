using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VideoPanel : BasePanel
{
    public Slider slider;
    protected override void Awake()
    {
        Init();
        base.Awake();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        InitSelectSlider("Video Slider");
    }
    public override void OnResume()
    {
        base.OnResume();
        InitSelectSlider("Video Slider");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.DestroyPanel(UIPanelType.VideoPanel);
    }
    private void Init()
    {
        slider.value = (float)GameManager.Instance.GameData.videoValue;
    }
    public void OnVideoValueSlider()
    {
        GameManager.Instance.GameData.videoValue = slider.value;
        GameManager.Instance.FixBrightness();
    }
    public override void OnPopPanel()
    {
        GameManager.Instance.Save();
        base.OnPopPanel();
    }
}
