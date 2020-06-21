using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Serialization;
using DG.Tweening;

public class EyeCamera : MonoBehaviour
{
    public GameObject flipGroup;
    public float duration = 5f;
    public float alpha = 0.5f;

    [Space]

    private bool flipFlag = false;
    private float timer = 0f;
    private RawImage ri;

    public bool FlipFlag { get => flipFlag; }

    private void Awake()
    {
        if (flipGroup != null)
            ri = flipGroup.GetComponentInChildren<RawImage>();
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameState.Gaming && Input.GetButtonDown("See"))
            PrepareFlip();
    }
    //**************************************************************
    //
    //          扫描实现
    //
    //**************************************************************
    private void PrepareFlip()
    {
        flipFlag = true;
        StopCoroutine("Wait");
        StopCoroutine("Countdown");
        if(flipGroup != null)
        {
            timer = duration;
            ri.material.SetFloat("_Alpha", alpha);
            flipGroup.SetActive(FlipFlag);
        }
        StartCoroutine("Countdown");
    }
    private IEnumerator Countdown()
    {
        yield return StartCoroutine("Wait");
        flipFlag = false;
        if (flipGroup != null)
            flipGroup.SetActive(FlipFlag);
    }
    private IEnumerator Wait()
    {
        for (timer = duration; timer > 0; timer -= Time.deltaTime)
        {
            if (flipGroup != null)
                ri.material.SetFloat("_Alpha", alpha * timer / duration);
            yield return 0;
        }
    }
}
