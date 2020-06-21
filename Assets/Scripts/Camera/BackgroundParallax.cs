using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Renderer[] backgrounds;
    public float parallaxScale = 1f;
    public float parallaxReductionFactor = .2f;
    public float smoothing = .7f;
    public float speed = 1.2f;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = GetComponentInParent<Camera>().transform;
        previousCamPos = cam.position;
    }
    private void Update()
    {
        //float parallax = ((cam.position.x - previousCamPos.x) + speed * Time.unscaledDeltaTime) * parallaxScale;
        float parallax = speed * Time.unscaledDeltaTime * parallaxScale;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);
            //Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            float targetPosX = backgrounds[i].material.mainTextureOffset.x + parallax * (i * parallaxReductionFactor + 1);
            Vector2 targetPos = new Vector2(targetPosX, backgrounds[i].material.mainTextureOffset.y);
            backgrounds[i].material.mainTextureOffset = Vector2.Lerp(backgrounds[i].material.mainTextureOffset, targetPos, smoothing * Time.unscaledDeltaTime);
        }

        previousCamPos = cam.position;
    }
}
