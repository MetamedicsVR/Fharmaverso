using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dragable : Interactable2D
{
    protected float distanceFromCamera;
    protected Vector3 positionOffset;

    protected override void InteractionStarted()
    {
        distanceFromCamera = Vector3.Project(transform.position - Camera.main.transform.position, Camera.main.transform.forward).magnitude;
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        positionOffset = transform.position - mouseWorldPosition;
    }

    private void Update()
    {
        if (interacting)
        {
            Drag();
        }
    }

    protected abstract void Drag();
}
