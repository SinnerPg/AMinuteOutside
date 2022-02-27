using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoManager : MonoBehaviour
{
    public BoxCollider2D colliderUp, colliderDx, colliderDown, colliderSx;
    public Animator animator;

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            reset();
        }    
    }

    public void firstStep()
    {
        colliderUp.enabled = true;
        colliderDx.enabled = true;
    }

    public void secondStep()
    {
        colliderUp.enabled = false;
        colliderDown.enabled = true;
    }

    public void thirdStep()
    {
        colliderDx.enabled = false;
        colliderSx.enabled = true;
    }

    public void fourthStep()
    {
        colliderDown.enabled = false;
        colliderUp.enabled = true;
    }

    void reset()
    {
        colliderUp.enabled = false;
        colliderDx.enabled = false;
        colliderDown.enabled = false;
        colliderSx.enabled = false;
    }
}
