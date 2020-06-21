using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class ViewDataController : MonoBehaviour
{
    public Camera cam;
    private ViewDataArea viewBase;
    
    void Start()
    {
        viewBase = new ViewDataArea();
    }
    
    void Update()
    {
        FixViewDatas();
    }

    private void FixViewDatas()
    {
        if (GameManager.Instance.viewDatas.Count != 0)
            GameManager.Instance.viewDatas.Clear();

        viewBase.LeftUp = new Vector2(cam.transform.position.x - cam.orthographicSize * cam.aspect,
                                        cam.transform.position.y + cam.orthographicSize);
        viewBase.RightDown = new Vector2(cam.transform.position.x + cam.orthographicSize * cam.aspect,
                                        cam.transform.position.y - cam.orthographicSize);

        if (GameManager.Instance.GameState == GameState.Gaming)
        {
            if (GameManager.Instance.Light.intensity <= 0.1)
            {
                UnityEngine.Experimental.Rendering.Universal.Light2D[] lights = FindObjectsOfType<UnityEngine.Experimental.Rendering.Universal.Light2D>();
                for(int i = 0; i < lights.Length; i++)
                {
                    if(lights[i].intensity > 0.3
                        && lights[i].transform.position.x > viewBase.LeftUp.x && lights[i].transform.position.x < viewBase.RightDown.x
                        && lights[i].transform.position.y > viewBase.RightDown.y && lights[i].transform.position.y < viewBase.LeftUp.y)
                    {
                        ViewData viewData = new ViewDataCircle(lights[i].transform.position, lights[i].pointLightOuterRadius);
                        if(!GameManager.Instance.viewDatas.Contains(viewData))
                            GameManager.Instance.viewDatas.Add(viewData);
                    }
                }
                return;
            }
        }

        GameManager.Instance.viewDatas.Add(viewBase);
    }
}
