using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShelterChecker : MonoBehaviour
{
    public bool canInteract;

    void Start() 
    {
        canInteract = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 7)
        {
            canInteract = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 7)
        {
            canInteract = true;
        }
    }
}
