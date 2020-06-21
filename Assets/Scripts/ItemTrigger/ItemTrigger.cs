using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemTrigger : MonoBehaviour
{
    public GameObject keyInfo;
    public float radius = 0.5f;
    private bool isTrigged = false;
    private bool isTriggedSave = false;
    protected virtual void Update()
    {
        isTrigged = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
                isTrigged = true;
        }

        if(isTrigged)
        {
            if(!isTriggedSave)
            {
                keyInfo.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            }

            if(GameManager.Instance.GameState == GameState.Gaming && Input.GetButtonDown("Interaction"))
            {
                OnItemTrigged();
            }
        } else if(isTriggedSave)
        {
            keyInfo.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
        }

        isTriggedSave = isTrigged;
    }

    protected virtual void OnItemTrigged() { }
}
