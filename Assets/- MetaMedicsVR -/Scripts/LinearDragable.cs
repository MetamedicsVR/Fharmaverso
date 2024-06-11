using UnityEngine;
using UnityEngine.Events;

public class LinearDragable : Dragable
{
    public UnityEvent Snapped;

    public Transform pointA;
    public Transform pointB;
    public bool resetOnRelease;
    [Header("Snap")]
    public bool shouldSnap;
    [Range(0, 1)]
    public float snapPoint;
    public float snapRange;
    public bool autoSnap;

    private Vector3 startingPosition;
    private bool snapped;

    private void Start()
    {
        startingPosition = transform.position;
    }

    protected override void InteractionEnded()
    {
        Release();
    }

    protected override void InteractionCanceled()
    {
        Release();
    }

    private void Release()
    {
        CheckSnap();
        if (snapped)
        {
            canInteract = false;
            Snapped.Invoke();
        }
        else
        {
            if (resetOnRelease)
            {
                transform.position = startingPosition;
            }
        }
    }

    protected override void Drag()
    {
        if (!snapped)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = distanceFromCamera;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            transform.position = ClosestPoint(mouseWorldPosition + positionOffset);
            if (autoSnap)
            {
                CheckSnap();
            }
        }
    }

    private Vector3 ClosestPoint(Vector3 p)
    {
        Vector3 AB = pointB.position - pointA.position;
        Vector3 AP = p - pointA.position;

        float magnitudeAB = AB.sqrMagnitude;
        float ABdotAP = Vector3.Dot(AP, AB);
        float t = ABdotAP / magnitudeAB;

        if (t < 0)
        {
            return pointA.position;
        }
        else if (t > 1)
        {
            return pointB.position;
        }
        Vector3 closestPoint = pointA.position + t * AB;
        return closestPoint;
    }

    private void CheckSnap()
    {
        if (shouldSnap)
        {
            Vector3 snapPosition = Vector3.Lerp(pointA.position, pointB.position, snapPoint);
            if (Vector3.Distance(transform.position, snapPosition) <= snapRange)
            {
                transform.position = snapPosition;
                snapped = true;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (pointA && pointB)
        {
            Gizmos.color = new Color(1, 1, 0);
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.color = new Color(1, 0.5f, 0, 0.5f);
            if (shouldSnap)
            {
                Gizmos.DrawSphere(Vector3.Lerp(pointA.position, pointB.position, snapPoint), snapRange);
            }
        }
    }
#endif
}
