using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class UIManager
{
    public bool hasFlipedPanel = false;

    private static readonly string UITypeFilePath = "UIPanelType";
    private Dictionary<UIPanelType, string> panelPathDict = new Dictionary<UIPanelType, string>();
    private Dictionary<UIPanelType, BasePanel> panelDict = new Dictionary<UIPanelType, BasePanel>();
    private Stack<BasePanel> panelStack = new Stack<BasePanel>();
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private Transform canvasTransform;
    public Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private UIManager()
    {
        ParseUIPanelTypeJson();
    }
    private void ParseUIPanelTypeJson()
    {
        TextAsset ta = Resources.Load<TextAsset>(UITypeFilePath);
        JsonData jsonDataArray = JsonMapper.ToObject(ta.text);
        foreach(JsonData jsonData in jsonDataArray)
        {
            UIPanelType panelType = (UIPanelType)Enum.Parse(typeof(UIPanelType), jsonData["panelType"].ToString());
            string path = jsonData["path"].ToString();
            panelPathDict.Add(panelType, path);
        }
    }
    private BasePanel GetPanel(UIPanelType panelType)
    {
        BasePanel basePanel;
        if(panelDict.TryGetValue(panelType, out basePanel))
        {
            return basePanel;
        } else
        {
            string path;
            panelPathDict.TryGetValue(panelType, out path);
            GameObject newPanel = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(path) as GameObject);
            panelDict.Add(panelType, newPanel.GetComponent<BasePanel>());
            return newPanel.GetComponent<BasePanel>();
        }
    }
    public void SetDefaultPopPanel(UIPanelType panelType, BasePanel basePanel)
    {
        panelDict.Add(panelType, basePanel);
        panelStack.Push(basePanel);
    }
    public void PushPanel(UIPanelType panelType)
    {
        if(panelStack.Count > 0)
        {
            panelStack.Peek().OnPause();
        }
        BasePanel panel = GetPanel(panelType);
        panel.transform.SetParent(CanvasTransform, false);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    public void PopPanel()
    {
        if (panelStack.Count <= 0)
            return;
        panelStack.Pop().OnExit();
        if (panelStack.Count <= 0)
            return;
        panelStack.Peek().OnResume();
    }
    public bool DestroyPanel(UIPanelType panelType)
    {
        return panelDict.Remove(panelType);
    }
    public void ClearPanel()
    {
        while(panelStack.Count > 0)
        {
            panelStack.Pop().OnExit();
        }
    }
}
