using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDoorTigger : ItemTrigger
{
    protected override void OnItemTrigged()
    {
        if(GameManager.Instance.GameData.keyNum >= 4)
        {
            GameManager.Instance.Save();
            GameManager.Instance.ReturnMenu();
        }
    }
}
