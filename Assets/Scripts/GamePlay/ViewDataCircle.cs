using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDataCircle : ViewData
{
    private Vector2 origin;
    public Vector2 Origin { get => origin; set => origin = value; }
    private float radius;
    public float Radius { get => radius; set => radius = value; }

    public ViewDataCircle() { }
    public ViewDataCircle(Vector2 origin, float radius)
    {
        Origin = origin;
        Radius = radius;
    }

    public override Collider2D[] GetObjects(int layerMask, float minDepth, float maxDepth)
    {
        return Physics2D.OverlapCircleAll(Origin, Radius, layerMask, minDepth, maxDepth);
    }
}
