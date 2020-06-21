using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDataArea : ViewData
{
    private Vector2 leftUp;
    public Vector2 LeftUp { get => leftUp; set => leftUp = value; }
    private Vector2 rightDown;
    public Vector2 RightDown { get => rightDown; set => rightDown = value; }

    public ViewDataArea() { }
    public ViewDataArea(Vector2 leftUp, Vector2 rightDown)
    {
        LeftUp = leftUp;
        RightDown = rightDown;
    }

    public override Collider2D[] GetObjects(int layerMask, float minDepth, float maxDepth)
    {
        return Physics2D.OverlapAreaAll(LeftUp, RightDown, layerMask, minDepth, maxDepth);
    }
}
