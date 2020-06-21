using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeCamera : MonoBehaviour
{
    void Update()
    {
        if(GameManager.Instance.GameState == GameState.Gaming && Input.GetButtonDown("Telescope"))
        {
            GameManager.Instance.SetPlayerCanMove(!GameManager.Instance.Player.GetComponent<PlayerMove>().canMove);
            GameManager.Instance.FreedomMove();
        }
    }
}
