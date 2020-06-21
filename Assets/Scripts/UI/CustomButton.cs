using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (interactable)
            AudioManager.Instance.Play("btnMoveSound");
    }
    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        //if(interactable)
        //    AudioManager.Instance.Play("btnDownSound");
    }
}
