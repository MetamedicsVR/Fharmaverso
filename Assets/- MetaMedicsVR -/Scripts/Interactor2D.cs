using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor2D : MonoBehaviour
{
    private Interactable2D selectedInteractable;
    private Interactable2D lastSelectedInteractable;
    private Interactable2D interactingInteractable;

    private void Update()
    {
        SelectInteractable();
        CheckInteractableChange();
        Interact();
        //Test();
    }

    private void SelectInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
        if (hit.collider)
        {
            print("Hit: " + hit.collider.name);
            selectedInteractable = hit.transform.gameObject.GetComponent<Interactable2D>();
        }
        else
        {
            print("Nothing");
            selectedInteractable = null;
        }
    }


    /*
    public Transform rows;
    public Color hitColor = new Color(1, 1, 0, 0.5f);
    public Color missColor = new Color(0, 1, 0, 0.5f);

    private void Test()
    {
        for (int i = 0; i < rows.childCount; i++)
        {
            for (int j = 0; j < rows.GetChild(i).childCount; j++)
            {
                Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(rows.GetChild(i).GetChild(j).position));
                Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity);
                if (hit.collider)
                {
                    rows.GetChild(i).GetChild(j).GetComponent<RawImage>().color = hitColor;
                }
                else
                {
                    rows.GetChild(i).GetChild(j).GetComponent<RawImage>().color = missColor;
                }
            }
        }
    }
    */

    private void CheckInteractableChange()
    {
        if (selectedInteractable != lastSelectedInteractable)
        {
            if (lastSelectedInteractable)
            {
                lastSelectedInteractable.EndHover();
            }
            if (selectedInteractable)
            {
                selectedInteractable.StartHover();
            }
            lastSelectedInteractable = selectedInteractable;
        }
    }

    private void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedInteractable)
            {
                interactingInteractable = selectedInteractable;
                interactingInteractable.StartInteraction();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (interactingInteractable)
            {
                if (interactingInteractable == selectedInteractable)
                {
                    interactingInteractable.EndInteraction();
                }
                else
                {
                    interactingInteractable.CancelInteraction();
                }
                interactingInteractable = null;
            }
        }
    }
}
