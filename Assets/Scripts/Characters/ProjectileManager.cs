using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int speed;
    public float timer;
    float disableTimer;
    string weapon;
    void Start()
    {
        disableTimer = timer;
    }
    void Update()
    {
        if(disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
        else
        {
            disableGameObject();
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed, Space.Self);
    }

    public void Shot(Vector3 direction, string weapon)
    {
        transform.eulerAngles = direction;
        this.weapon = weapon;
    }
    public void disableGameObject()
    {
        gameObject.SetActive(false);
        this.enabled = false;
    }

    void OnEnable()
    {
        disableTimer = timer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 7)
        {
            switch(weapon)
            {
                case "SMG":
                    other.gameObject.GetComponentInChildren<EnemyManagerJobs>().changeHp(-InventoryManager.SMG.getDamage(), weapon);
                    disableGameObject();
                    break;
                case "SG":
                    other.gameObject.GetComponentInChildren<EnemyManagerJobs>().changeHp(-InventoryManager.SG.getDamage() * 0.2f, weapon);
                    disableGameObject();
                    break;
            }
        }

        if(1 << other.gameObject.layer == 1 << 11)
        {
            disableGameObject();
        }
    }    
}
