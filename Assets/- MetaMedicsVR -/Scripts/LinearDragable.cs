using UnityEngine;
using UnityEngine.Events;

public class LinearDragable : Dragable
{
    public UnityEvent Snapped;
    public UnityEvent<float> OnDisplacementChanged;

    public Transform pointA;
    public Transform pointB;
    public bool resetOnRelease;

    [Header("Snap")]
    public bool shouldSnap;
    [Range(0, 1)]
    public float snapPoint;
    public float snapRange;
    public bool autoSnap;

    private float currentDisplacement = 0;
    private float lastDisplacement = 0;

    [Header("Animations")]
    public AnimationBlend[] animationBlends;

    private Vector3 startingPosition;
    private bool snapped;

    [System.Serializable]
    public struct AnimationBlend
    {
        public Animator animator;
        public string animationName;
        public bool reverse;
    }

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
            CheckDisplacement(mouseWorldPosition + positionOffset);
            if (autoSnap)
            {
                CheckSnap();
            }
            if (currentDisplacement != lastDisplacement)
            {
                transform.position = pointA.position + currentDisplacement * (pointB.position - pointA.position);
                UpdateAnimations();
                OnDisplacementChanged.Invoke(currentDisplacement);
                lastDisplacement = currentDisplacement;
            }
        }
    }

    private void CheckDisplacement(Vector3 p)
    {
        Vector3 AB = pointB.position - pointA.position;
        Vector3 AP = p - pointA.position;

        float magnitudeAB = AB.sqrMagnitude;
        float ABdotAP = Vector3.Dot(AP, AB);
        currentDisplacement = Mathf.Clamp01(ABdotAP / magnitudeAB);
    }

    private void UpdateAnimations()
    {
        if (animationBlends.Length > 0)
        {
            float time = Mathf.Clamp(currentDisplacement, 0.0001f, 0.9999f);
            foreach (AnimationBlend animationBlend in animationBlends)
            {
                if (animationBlend.animator && animationBlend.animationName != "")
                {
                    animationBlend.animator.speed = 0;
                    animationBlend.animator.Play(animationBlend.animationName, 0, animationBlend.reverse ? 1 - time : time);
                }
            }
        }
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
                currentDisplacement = 1;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
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
