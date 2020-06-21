using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIFlipCallback : FlipCallback
{
    protected BasePanel panel;
    protected void Awake()
    {
        panel = GetComponent<BasePanel>();
    }
    protected bool CanCallBack()
    {
        return (UIManager.Instance.hasFlipedPanel && panel.isFilped) || (!UIManager.Instance.hasFlipedPanel && panel.IsActive);
    }
}
