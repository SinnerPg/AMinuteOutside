using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    public float smgAtkRatio;
    public float sgAtkRatio;
    float timerSMG, timerSG;
    [HideInInspector]
    public bool canShootSMG = true;
    [HideInInspector]
    public bool canShootSG = true;
    void Awake()
    {
        timerSMG = smgAtkRatio;
        timerSG = sgAtkRatio;
    }

    void Update()
    {
        if(!canShootSMG)
        {
            timerSMG -= Time.deltaTime;
            if(timerSMG <= 0)
            {
                canShootSMG = true;
                timerSMG = smgAtkRatio;
            }
        }

        if(!canShootSG)
        {
            timerSG -= Time.deltaTime;
            if(timerSG <= 0)
            {
                canShootSG = true;
                timerSG = sgAtkRatio;
            }
        }
    }
}
