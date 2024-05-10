using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNextAnimation : MonoBehaviour
{
    public Animator animator;

    public void SetNext()
    {
        animator.SetInteger("NextAnimation", Random.Range(0, 3));
    }
}
