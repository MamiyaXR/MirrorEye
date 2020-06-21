using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewData
{
    public abstract Collider2D[] GetObjects(int layerMask, float minDepth, float maxDepth);
}
