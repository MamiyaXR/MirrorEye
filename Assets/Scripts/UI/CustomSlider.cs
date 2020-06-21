using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomSlider : Slider
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (interactable)
            AudioManager.Instance.Play("btnMoveSound");
    }
}
