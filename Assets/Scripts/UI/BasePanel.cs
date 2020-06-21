using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public enum UIPanelType : int
{
    MainPanel,
    PausePanel,
    OptionPanel,
    StagePanel,
    AudioPanel,
    VideoPanel
}
public abstract class BasePanel : MonoBehaviour
{
    public bool isFilped = false;
    protected bool isActive = false;
    public bool IsActive { get => isActive; }
    protected CanvasGroup canvasGroup;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    protected virtual void OnDestroy()
    {
        UIManager.Instance.PopPanel();
    }
    public virtual void OnEnter()
    {
        //gameObject.SetActive(true);
        isActive = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        AudioManager.Instance.Play("openMenuSound");

        transform.localScale = new Vector3(0f, 0f, 0f);
        Tween tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        tween.SetUpdate(true);
    }
    public virtual void OnPause()
    {
        isActive = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        //gameObject.SetActive(false);
    }
    public virtual void OnResume()
    {
        //gameObject.SetActive(true);
        isActive = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        AudioManager.Instance.Play("openMenuSound");

        transform.localScale = new Vector3(0f, 0f, 0f);
        Tween tween = transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        tween.SetUpdate(true);
    }
    public virtual void OnExit()
    {
        isActive = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        //gameObject.SetActive(false);
    }
    protected bool InitSelectButton(Transform trans, string name)
    {
        Transform button;
        for (int i = 0; i < trans.childCount; i++)
        {
            button = trans.GetChild(i);
            if (button.name == name)
            {
                button.GetComponent<Button>().Select();
                return true;
            } else if(InitSelectButton(button, name))
            {
                return true;
            }
        }
        return false;
    }
    protected void InitSelectSlider(string name)
    {
        GameObject slider;
        for (int i = 0; i < transform.childCount; i++)
        {
            slider = transform.GetChild(i).gameObject;
            if (slider.name == name)
            {
                slider.GetComponent<Slider>().Select();
                break;
            }
        }
    }
    public virtual void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
    public virtual void OnPopPanel()
    {
        UIManager.Instance.PopPanel();
    }
}
