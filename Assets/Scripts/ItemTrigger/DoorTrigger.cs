using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : ItemTrigger
{
    public Direct direct;
    protected override void OnItemTrigged()
    {
        GameManager.Instance.NextStage(direct);
    }
}
