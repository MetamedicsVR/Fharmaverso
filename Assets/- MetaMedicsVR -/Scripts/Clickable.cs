using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : Interactable2D
{
    public UnityEvent OnClickStart;
    public UnityEvent OnClickCompleted;
    public UnityEvent OnClickCanceled;

    protected override void InteractionStarted()
    {
        OnClickStart.Invoke();
    }

    protected override void InteractionEnded()
    {
        OnClickCompleted.Invoke();
    }

    protected override void InteractionCanceled()
    {
        OnClickCanceled.Invoke();
    }
}
