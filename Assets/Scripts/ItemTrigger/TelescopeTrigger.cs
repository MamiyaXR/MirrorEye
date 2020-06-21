using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeTrigger : ItemTrigger
{
    protected override void OnItemTrigged()
    {
        Camera.main.gameObject.GetComponent<EyeCameraControl>().AddTelescope(); ;
        DestroyImmediate(gameObject);
    }
}
