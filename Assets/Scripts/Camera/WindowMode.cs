using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowMode : MonoBehaviour
{
    public Camera cam;
    /// 开发屏幕的宽
    public float DevelopWidth = 1280f;
    /// 开发屏幕的长
    public float DevelopHeigh = 720f;
    /// 开发高宽比
    private float DevelopRate;
    /// 设备自身的高
    private int curScreenHeight;
    /// 设备自身的高
    public int curScreenWidth;
    /// 当前屏幕高宽比
    private float ScreenRate;
    /// 世界摄像机rect高的比例
    private float cameraRectHeightRate;
    /// 世界摄像机rect宽的比例
    private float cameraRectWidthRate;
    private void Awake()
    {
        DevelopRate = DevelopHeigh / DevelopWidth;
        
        FitCamera(cam);
    }
    private void Update()
    {
        FitCamera(cam);
    }
    public void FitCamera(Camera camera)
    {
        curScreenHeight = Screen.height;
        curScreenWidth = Screen.width;
        ScreenRate = (float)Screen.height / (float)Screen.width;
        cameraRectHeightRate = DevelopHeigh / ((DevelopWidth / Screen.width) * Screen.height);
        cameraRectWidthRate = DevelopWidth / ((DevelopHeigh / Screen.height) * Screen.width);
        ///适配屏幕。实际屏幕比例<=开发比例的 上下黑  反之左右黑
        if (DevelopRate <= ScreenRate)
        {
            camera.rect = new Rect(0, (1 - cameraRectHeightRate) / 2, 1, cameraRectHeightRate);
        }
        else
        {
            camera.rect = new Rect((1 - cameraRectWidthRate) / 2, 0, cameraRectWidthRate, 1);
        }
    }
}
