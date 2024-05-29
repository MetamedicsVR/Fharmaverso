using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void SelectInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
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
