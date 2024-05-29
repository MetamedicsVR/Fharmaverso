using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDragable : Dragable
{
    protected override void Drag()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.position = mouseWorldPosition + positionOffset;
    }
}
