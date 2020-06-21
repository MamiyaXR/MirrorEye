using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LampTrigger : ItemTrigger
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D light;
    private void Awake()
    {
        light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    protected override void OnItemTrigged()
    {
        if (light.intensity != 0)
            light.intensity = 0;
        else
            light.intensity = 1;
    }
}
