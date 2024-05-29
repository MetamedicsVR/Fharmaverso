using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircularDragable : Dragable
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public struct Circle
    {
        public Vector3 center;
        public Vector3 normal;
        public float radius;
    }

    protected override void Drag()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = distanceFromCamera;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        //transform.position = ClosestPoint(mouseWorldPosition + positionOffset);
    }

    public Circle CalculateCircle()
    {
        Circle circle = new Circle();

        Vector3 AB = pointB.position - pointA.position;
        Vector3 AC = pointC.position - pointA.position;

        Vector3 midAB = (pointA.position + pointB.position) / 2;
        Vector3 midAC = (pointA.position + pointC.position) / 2;

        Plane plane = new Plane(pointA.position, pointB.position, pointC.position);

        Vector3 perpAB = Vector3.Cross(plane.normal, AB).normalized;
        Vector3 perpBC = Vector3.Cross(plane.normal, AC).normalized;

        Vector3 diffMid = midAC - midAB;
        Vector3 crossPerp = Vector3.Cross(perpAB, perpBC);

        float t = Vector3.Dot(Vector3.Cross(diffMid, perpBC), crossPerp) / crossPerp.magnitude;

        circle.center = midAB + t * perpAB;
        circle.normal = plane.normal;
        circle.radius = Vector3.Distance(pointA.position, circle.center);
        Gizmos.DrawSphere(circle.center, 1f);
        return circle;
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (pointA && pointB && pointC)
        {
            Circle circle = CalculateCircle();
            float angle = Vector3.SignedAngle(pointA.position - circle.center, pointB.position - circle.center, circle.normal);
            Handles.color = new Color(1, 1, 0);
            Handles.DrawWireArc(circle.center, circle.normal, pointA.position - circle.center, angle, circle.radius);
        }
    }
#endif
}
