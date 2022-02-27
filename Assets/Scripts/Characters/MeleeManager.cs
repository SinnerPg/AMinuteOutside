using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 7)
        {
            other.GetComponentInChildren<EnemyManagerJobs>().changeHp(-InventoryManager.Melee.getDamage(), "Melee");
        }
    }
}
