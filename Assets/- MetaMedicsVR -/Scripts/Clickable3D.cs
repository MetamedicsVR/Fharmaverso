using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable3D : MonoBehaviour
{
    public UnityEvent clicked = new UnityEvent();

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    clicked.Invoke();
                }
            }
        }
    }
}
