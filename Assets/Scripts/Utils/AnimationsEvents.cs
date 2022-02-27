using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    public GameObject deathResume, deathMenu;
    public void disableGameObject()
    {
        this.gameObject.SetActive(false);
    }
    public void goIdle()
    {
        GetComponent<Animator>().Play("idle");
    }
    public void enableDeathButtons()
    {
        deathResume.SetActive(true);
        deathMenu.SetActive(true);
    }
}
