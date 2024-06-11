using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class Interactable2D : MonoBehaviour
{
    public bool canInteract = true;
    public UnityEvent OnHoverStart;
    public UnityEvent OnHoverEnd;

    protected bool hovering { get; private set; }
    protected bool interacting { get; private set; }

    public virtual void StartHover()
    {
        hovering = true;
        OnHoverStart.Invoke();
    }

    public virtual void EndHover()
    {
        hovering = false;
        OnHoverEnd.Invoke();
    }

    public void StartInteraction()
    {
        if (!interacting && canInteract)
        {
            interacting = true;
            InteractionStarted();
        }
    }

    protected virtual void InteractionStarted()
    {
    
    }

    public void EndInteraction()
    {
        if (interacting)
        {
            interacting = false;
            InteractionEnded();
        }
    }

    protected virtual void InteractionEnded()
    {
    
    }

    public void CancelInteraction()
    {
        if (interacting)
        {
            interacting = false;
            InteractionCanceled();
        }
    }

    protected virtual void InteractionCanceled()
    {

    }
}
