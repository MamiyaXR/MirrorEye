using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFlipCallBack : FlipCallback
{
    public override void OnFlipCallback()
    {
        GameManager.Instance.isLSFliped = !GameManager.Instance.isLSFliped;
    }
}
