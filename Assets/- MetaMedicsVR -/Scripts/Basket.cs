using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public Game_FallingObjects fallingObjects;

    private void OnTriggerEnter(Collider other)
    {
        FallingItem fallingItem = other.GetComponent<FallingItem>();
        if (fallingItem)
        {
            fallingObjects.GotItem(fallingItem);
        }
    }
}
