using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.GameData.keyNum++;
            GameManager.Instance.GameData.levelDataDict[Enum.GetName(typeof(LevelType), (LevelType)GameManager.Instance.GameData.stageCurrent)].hasKey = false;
            GameManager.Instance.Save();
            Camera.main.GetComponent<EyeCameraControl>().ChangeKeysIcon();
            Destroy(gameObject);
        }
    }
}
