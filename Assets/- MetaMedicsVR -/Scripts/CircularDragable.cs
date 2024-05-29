using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CircularDragable : Dragable
{
    public UnityEvent OnDragEnded;

    [Header("Circle Points")]
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    [Header("Look Center")]
    public bool lookCenter;
    [Range(0, 360)]
    public float lookAngle;

    private Vector3 lastPositionA;
    private Vector3 lastPositionB;
    private Vector3 lastPositionC;
    private Circle circle;
    private float arcAngle;

    public struct Circle
    {
        public Vector3 center;
        public Vector3 normal;
        public float radius;
    }

    private void Awake()
    {
        CheckCircleChange();
    }

    protected override void InteractionEnded()
    {
        OnDragEnded.Invoke();
    }

    public Circle GetCircle()
    {
        return circle;
    }

    private void CheckCircleChange()
    {
        if (lastPositionA != pointA.position || lastPositionB != pointB.position || lastPositionC != pointC.position)
        {
            circle = CalculateCircle();
            arcAngle = CalculateArcAngle();
            lastPositionA = pointA.position;
            lastPositionB = pointB.position;
            lastPositionC = pointC.position;
        }
    }

    protected override void Drag()
    {
        CheckCircleChange();
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        transform.position = ClosestPoint(mouseWorldPosition + positionOffset);
        if (lookCenter)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, lookAngle - Vector3.SignedAngle(Vector3.ProjectOnPlane(pointA.position - circle.center, Vector3.up), transform.position - circle.center, circle.normal));
        }
    }

    private Vector3 ClosestPoint(Vector3 p)
    {
        Vector3 vectorA = pointA.position - circle.center;
        Vector3 vectorP = Vector3.ProjectOnPlane(p - circle.center, circle.normal);
        float angleAP = Vector3.SignedAngle(vectorA, vectorP, circle.normal);
        if (Mathf.Sign(angleAP) != Mathf.Sign(arcAngle))
        {
            if (angleAP < 0)
            {
                angleAP += 360f;
            }
            else
            {
                angleAP -= 360;
            }
        }
        if (Mathf.Abs(angleAP) < Mathf.Abs(arcAngle))
        {
            return circle.center + Quaternion.AngleAxis(angleAP, circle.normal) * (pointA.position - circle.center);
        }
        if (Vector3.Distance(p, pointA.position) < Vector3.Distance(p, pointB.position))
        {
            return pointA.position;
        }
        return pointB.position;
    }

    public Circle CalculateCircle()
    {
        Circle circle = new Circle();

        Vector3 AB = pointB.position - pointA.position;
        Vector3 AC = pointC.position - pointA.position;

        Plane circlePlane = new Plane(pointA.position, pointB.position, pointC.position);
        Plane ABMidPlane = new Plane(AB, (pointA.position + pointB.position) / 2);
        Plane ACMidPlane = new Plane(AC, (pointA.position + pointC.position) / 2);

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, circlePlane.normal);
        matrix.SetRow(1, ABMidPlane.normal);
        matrix.SetRow(2, ACMidPlane.normal);
        matrix[3, 3] = 1;
        Vector4 distances = new Vector4(-circlePlane.distance, -ABMidPlane.distance, -ACMidPlane.distance, 0);
        Vector4 solution = matrix.inverse * distances;

        circle.center = new Vector3(solution.x, solution.y, solution.z);
        circle.normal = circlePlane.normal;
        circle.radius = Vector3.Distance(pointA.position, circle.center);
        return circle;
    }

    public float CalculateArcAngle()
    {
        Vector3 vectorA = pointA.position - circle.center;
        Vector3 vectorB = pointB.position - circle.center;
        Vector3 vectorC = pointC.position - circle.center;

        float angleAB = Vector3.SignedAngle(vectorA, vectorB, circle.normal);
        float angleAC = Vector3.SignedAngle(vectorA, vectorC, circle.normal);

        if (angleAB < 0)
        {
            if (angleAC < 0 && angleAB < angleAC)
            {
                angleAB += 360f;
            }
        }
        else
        {
            if (angleAC > 0 && angleAB > angleAC)
            {
                angleAB -= 360f;
            }
        }
        return angleAB;
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (pointA && pointB && pointC)
        {
            CheckCircleChange();
            Gizmos.color = new Color(1, 1, 0, 0.5f);
            Gizmos.DrawSphere(circle.center, 1f);
            Handles.color = new Color(1, 1, 0);
            Handles.DrawWireArc(circle.center, circle.normal, pointA.position - circle.center, arcAngle, circle.radius);
        }
    }
#endif
}
