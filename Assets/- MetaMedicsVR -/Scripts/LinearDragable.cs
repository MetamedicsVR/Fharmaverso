using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LinearDragable : Dragable
{
    public UnityEvent Snapped;
    public UnityEvent<float> OnDisplacementChanged;

    public Transform pointA;
    public Transform pointB;
    public bool resetOnRelease;

    private Vector3 startingPosition;
    private float currentDisplacement = 0;
    private float lastDisplacement = 0;

    [Header("Snap")]
    public bool shouldSnap;
    [Range(0, 1)]
    public float snapPoint;
    public float snapRange;
    public bool autoSnap;

    private bool snapped;

    [Header("Slider")]
    public GameObject sliderPrefab;
    public float sliderSize = 1;
    public Vector3 sliderDisplacement;

    private GameObject instancedSlider;

    [Header("Animations")]
    public AnimationBlend[] animationBlends;
    public bool animationOnSnap;

    [System.Serializable]
    public struct AnimationBlend
    {
        public Animator animator;
        public string animationName;
    }

    private void Start()
    {
        startingPosition = transform.position;
        if (sliderPrefab)
        {
            Vector2 screenPointA = Camera.main.WorldToScreenPoint(pointA.position);
            Vector2 screenPointB = Camera.main.WorldToScreenPoint(pointB.position);
            float angle = Mathf.Atan2(screenPointB.y - screenPointA.y, screenPointB.x - screenPointA.x) * Mathf.Rad2Deg;
            instancedSlider = Instantiate(sliderPrefab, (pointA.position + pointB.position) / 2 + sliderDisplacement, Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, angle));
            instancedSlider.transform.parent = transform.parent;

            float cameraDistance = Camera.main.transform.InverseTransformPoint(instancedSlider.transform.position).z;
            RectTransform sliderScale = instancedSlider.transform.GetChild(0).GetComponent<RectTransform>();
            sliderScale.localScale = new Vector2(sliderSize * cameraDistance, sliderSize * cameraDistance);
            RectTransform sliderChild = instancedSlider.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
            float distance = (pointB.position - pointA.position).magnitude;
            sliderChild.sizeDelta = new Vector2((sliderChild.sizeDelta.x + 80) * distance / (1000 * sliderSize * cameraDistance), sliderChild.sizeDelta.y);
            Slider sliderComponent = sliderChild.GetComponent<Slider>();
            if (sliderComponent)
            {
                OnDisplacementChanged.AddListener((v) => sliderComponent.value = v);
            }
        }
        foreach (AnimationBlend animationBlend in animationBlends)
        {
            if (animationBlend.animator && animationBlend.animationName != "")
            {
                animationBlend.animator.speed = 0;
                animationBlend.animator.Play(animationBlend.animationName, 0, 0.0001f);
            }
        }
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
            if (!animationOnSnap)
            {
                Snapped.Invoke();
            }
            if (instancedSlider)
            {
                Destroy(instancedSlider);
            }
        }
        else
        {
            if (resetOnRelease)
            {
                transform.position = startingPosition;
                UpdateAnimations();
                OnDisplacementChanged.Invoke(0);
                lastDisplacement = 0;
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
                    animationBlend.animator.Play(animationBlend.animationName, 0, time);
                }
            }
        }
    }

    private void CheckSnap()
    {
        if (shouldSnap && !snapped)
        {
            Vector3 snapPosition = Vector3.Lerp(pointA.position, pointB.position, snapPoint);
            if (Vector3.Distance(transform.position, snapPosition) <= snapRange)
            {
                transform.position = snapPosition;
                snapped = true;
                currentDisplacement = snapPoint;
                if (animationOnSnap)
                {
                    StartCoroutine(AnimationAfterSnap());
                }
            }
        }
    }

    private IEnumerator AnimationAfterSnap()
    {
        float timeToEnd = 0;
        AnimatorStateInfo currentState;
        foreach (AnimationBlend animationBlend in animationBlends)
        {
            if (animationBlend.animator && animationBlend.animationName != "")
            {
                animationBlend.animator.speed = 1;
            }
        }
        yield return null;
        foreach (AnimationBlend animationBlend in animationBlends)
        {
            if (animationBlend.animator && animationBlend.animationName != "")
            {
                currentState = animationBlend.animator.GetCurrentAnimatorStateInfo(0);
                timeToEnd = Mathf.Max(timeToEnd, currentState.length * (1 - currentState.normalizedTime));
            }
        }
        yield return new WaitForSeconds(timeToEnd);
        Snapped.Invoke();
    }

#if UNITY_EDITOR

    [Header("Gizmos")]
    public float radius = 0.5f;
    public int segments = 20;

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
            if (sliderPrefab)
            {
                Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                DrawCylinder(pointA.position + sliderDisplacement, pointB.position + sliderDisplacement, radius, segments);
            }
            
        }
    }

    private void DrawCylinder(Vector3 start, Vector3 end, float radius, int segments)
    {
        Vector3 direction = end - start;
        float height = direction.magnitude;
        Vector3 up = direction.normalized;

        Vector3 right = Vector3.Cross(up, Vector3.forward).normalized * radius;
        Vector3 forward = Vector3.Cross(right, up).normalized * radius;

        DrawCircle(start, right, forward, up, segments);
        DrawCircle(end, right, forward, up, segments);

        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * 360f / segments;
            float angle2 = (i + 1) * 360f / segments;

            Vector3 offset1 = right * Mathf.Cos(angle1 * Mathf.Deg2Rad) + forward * Mathf.Sin(angle1 * Mathf.Deg2Rad);
            Vector3 offset2 = right * Mathf.Cos(angle2 * Mathf.Deg2Rad) + forward * Mathf.Sin(angle2 * Mathf.Deg2Rad);

            Vector3 point1 = start + offset1;
            Vector3 point2 = start + offset2;
            Vector3 point3 = end + offset1;
            Vector3 point4 = end + offset2;

            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point1, point3);
            Gizmos.DrawLine(point3, point4);
            Gizmos.DrawLine(point2, point4);
        }
    }

    private void DrawCircle(Vector3 center, Vector3 right, Vector3 forward, Vector3 up, int segments)
    {
        Vector3 lastPoint = center + right;
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 360f / segments;
            Vector3 offset = right * Mathf.Cos(angle * Mathf.Deg2Rad) + forward * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 nextPoint = center + offset;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
    }
#endif
}
