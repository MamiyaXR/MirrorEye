using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour
{
    public Slider progress;
    public Text loadingText;
    public float smooth = 2f;
    private float progressValue = 0f;
    private AsyncOperation operation;

    private float offset = 0f;
    private RectTransform rt;

    private void Awake()
    {
        progress.value = .0f;
        if (GameManager.Instance.activeGameScene.IsValid())
            SceneManager.UnloadSceneAsync(GameManager.Instance.activeGameScene);
        if(GameManager.Instance.NextScene == GameManager.Instance.GetSceneName(LevelType.Level02))
        {
            rt = progress.gameObject.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x * 1.5f, rt.sizeDelta.y);
            progress.maxValue = 1.5f;
            offset = 0.5f;
        }
        StartCoroutine(AsyncLoading());
    }
    private IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync("Scenes/" + GameManager.Instance.NextScene, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        yield return StartCoroutine("WaitLoad", operation);
        GameManager.Instance.ChangeGameState(GameManager.Instance.NextScene);
        GameManager.Instance.activeGameScene = SceneManager.GetSceneByName(GameManager.Instance.NextScene);
        GameManager.Instance.BreakFlip();
        SceneManager.UnloadSceneAsync("LoadScene");
    }
    private IEnumerator WaitLoad(AsyncOperation operation)
    {
        while (!operation.isDone)
            yield return 0;
    }
    private void Update()
    {
        if (operation == null)
            return;

        if (operation.progress < 0.9f)
        {
            progressValue = operation.progress + offset;
        } else
        {
            progressValue = 1.0f + offset;
        }

        float targetValue = GameManager.Instance.isLSFliped ? (progress.value >= 1 ? 1 : 0) : progressValue;
        if (progress.value != targetValue)
            progress.value = Mathf.Lerp(progress.value, targetValue, smooth * Time.deltaTime);
        if (Mathf.Abs(progress.value - targetValue) < 0.1f)
            progress.value = targetValue;

        if ((int)progress.value * 100 >= 100)
        {
            loadingText.GetComponent<Text>().text = "ANY BUTTON TO CONTINUE";
            if(Input.anyKeyDown && !Input.GetButtonDown("Flip"))
            {
                GameManager.Instance.lsProgress = progress.value;
                operation.allowSceneActivation = true;
            }
        }
    }
}
