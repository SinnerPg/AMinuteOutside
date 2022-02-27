using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    public EnemyManagerJobs enemyManager;
    private void OnTriggerEnter2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            other.GetComponent<PlayerManager>().changeHp(-enemyManager.damageHp);
            other.GetComponent<PlayerManager>().rad -= enemyManager.damageRad;
        }
    }
}
