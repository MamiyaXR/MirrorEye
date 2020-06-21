using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeCameraControl : MonoBehaviour
{
    public Image KeyImage;
    public Image ItemImage;
    public Sprite[] keys;
    public TelescopeCamera tc;

    private CameraMove cm;
    private EyeCamera ec;

    private void Awake()
    {
        ec = GetComponent<EyeCamera>();
        cm = GetComponent<CameraMove>();
        if (!GameManager.Instance.GameData.hasTelescope)
        {
            ItemImage.gameObject.SetActive(false);
            DestroyImmediate(tc);
        }
        ChangeKeysIcon();
    }
    private void Update()
    {
        if(cm != null)
        {
            if(ec.FlipFlag)
            {
                cm.moveMode = cm.moveMode | MoveMode.Move;
                cm.moveMode = cm.moveMode & ~MoveMode.Follow;
            } else
            {
                cm.moveMode = cm.moveMode | MoveMode.Move;
                cm.moveMode = cm.moveMode | MoveMode.Follow;
            }
        }
    }
    public void AddTelescope()
    {
        GameManager.Instance.GameData.hasTelescope = true;
        ItemImage.gameObject.SetActive(true);
        gameObject.AddComponent<TelescopeCamera>();
    }
    public void ChangeKeysIcon()
    {
        KeyImage.sprite = keys[GameManager.Instance.GameData.keyNum];
    }
}
