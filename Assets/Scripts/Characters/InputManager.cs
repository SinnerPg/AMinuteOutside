using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public bool walkRight = false;
    [HideInInspector] public bool walkLeft = false;
    [HideInInspector] public bool walkUp = false;
    [HideInInspector] public bool walkDown = false;
    [HideInInspector] public bool isShooting = false;
    [HideInInspector] public bool isAiming = false;
    [HideInInspector] public bool changeInventoryUp = false;
    [HideInInspector] public bool changeInventoryDown = false;
    [HideInInspector] public bool isDashing = false;
    [HideInInspector] public bool isPausing = false;
    void Update()
    {
        //Walk Right
        if(Input.GetKey(KeyCode.D))
        {
            walkRight = true;
        }
        else
        {
            walkRight = false;
        }

        //Walk Left
        if(Input.GetKey(KeyCode.A))
        {
            walkLeft = true;
        }
        else
        {
            walkLeft = false;
        }

        //Walk Down
        if(Input.GetKey(KeyCode.S))
        {
            walkDown = true;
        }
        else
        {
            walkDown = false;
        }

        //Walk Up
        if(Input.GetKey(KeyCode.W))
        {
            walkUp = true;
        }
        else
        {
            walkUp = false;
        }

        //Aim
        if(Input.GetMouseButton(1))
        {
            isAiming = true;
            //Shoot Aiming
            if(Input.GetMouseButton(0))
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }
        }
        else
        {
            isAiming = false;
            //Shoot
            if(Input.GetMouseButtonDown(0))
            {
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }
        }
        
        //Scroll inventory
        if(Input.mouseScrollDelta.y > 0)
        {
            changeInventoryUp = true;
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            changeInventoryDown = true;
        }
        else
        {
            changeInventoryDown = false;
            changeInventoryUp = false;
        }

        //Dash
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        //Pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPausing = !isPausing;
        }
    }
}
