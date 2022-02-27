using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShelter : MonoBehaviour
{
    InteractionManager interactionManager;
    public EnemyShelterChecker enemyShelterChecker;
    public bool canInteract;
    void Start()
    {
        interactionManager = GameObject.Find("GameDirector").GetComponent<InteractionManager>();
    }

    void Update()
    {
        if(canInteract && enemyShelterChecker.canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactionManager.finishGame();
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            canInteract = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
    }
}
