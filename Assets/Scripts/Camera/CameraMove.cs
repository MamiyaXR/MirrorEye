using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum MoveMode : int
{
    Move = 0x0001,
    Follow = 0x0010,
    FreeMove = 0x0100
}

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform LeftCheck;
    [SerializeField] private Transform RightCheck;
    [SerializeField] private GameObject border;

    [Space]

    [Header("MOVE & FOLLOW")]
    public MoveMode moveMode = MoveMode.Move | MoveMode.Follow;

    [Space]

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float moveBoder = 13.0f;

    [Space]

    [SerializeField] private float smoothFollow = 5f;

    private float minPosx;
    private float maxPosx;
    private float cameraMovement = 0;
    private GameObject followTarget;
    private Vector3 oldPos;

    private void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player");
        float width = GetComponent<Camera>().orthographicSize * 2 * GetComponent<Camera>().aspect;
        if (LeftCheck != null && RightCheck != null)
        {
            minPosx = LeftCheck.position.x + width / 2;
            maxPosx = RightCheck.position.x - width / 2;
        }
        if (GameManager.Instance.playerDirect == Direct.Left)
            transform.position = new Vector3(maxPosx, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(minPosx, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        cameraMovement = Input.GetAxis("Horizontal_R") * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (cameraMovement != 0)
            Move(cameraMovement);
        else
            Follow();
    }

    public void Move(float move)
    {
        if ((moveMode & MoveMode.Move) == 0)
            return;

        float cPosX = transform.position.x;
        Vector3 newCPos = transform.position;
        newCPos.x = cPosX + move * moveSpeed;
        if (newCPos.x >= maxPosx || newCPos.x <= minPosx)
            return;


        if (followTarget != null && ((moveMode & MoveMode.FreeMove) == 0))
        {
            float max = followTarget.transform.position.x + moveBoder;
            float min = followTarget.transform.position.x - moveBoder;
            if (max > maxPosx)
                max = maxPosx;
            if (min < minPosx)
                min = minPosx;

            if(max >= min)
                newCPos.x = Mathf.Clamp(newCPos.x, min, max);
            else
                newCPos.x = Mathf.Clamp(newCPos.x, max, min);
        } else if((moveMode & MoveMode.FreeMove) != 0)
        {
            newCPos.x = Mathf.Clamp(newCPos.x, minPosx, maxPosx);
        }

        transform.position = newCPos;
    }
    public void Follow()
    {
        if (followTarget == null)
            return;
        if ((moveMode & MoveMode.Follow) == 0 || (moveMode & MoveMode.FreeMove) != 0)
            return;

        float tPosX = followTarget.transform.position.x;
        float cPosX = transform.position.x;
        Vector3 newCPos = new Vector3(cPosX, transform.position.y, transform.position.z);

        if (tPosX - cPosX > distance)
            newCPos.x = tPosX - distance;
        if (tPosX - cPosX < -1 * distance)
            newCPos.x = tPosX + distance;

        newCPos.x = Mathf.Clamp(newCPos.x, minPosx, maxPosx);

        transform.position = Vector3.Lerp(transform.position, newCPos, smoothFollow * Time.deltaTime);
    }
    public void FreedomMove()
    {
        if (border == null)
            return;
        
        bool state = border.activeSelf;
        if (state)
        {
            moveMode = moveMode | MoveMode.FreeMove;
            oldPos = transform.position;
        } else
        {
            moveMode = moveMode & ~MoveMode.FreeMove;
            transform.position = oldPos;
        }
        border.SetActive(!state);
    }
}
