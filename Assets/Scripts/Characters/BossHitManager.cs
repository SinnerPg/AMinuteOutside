using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitManager : MonoBehaviour
{
    GameObject player;
    PlayerManager player_script;
    Vector2 direction;
    float disableTimer;
    int damage, hitpoints = 1;

    private void Start()
    {
        player = GameObject.Find("Protagonista");
        direction = (player.transform.position - transform.position).normalized;
        player_script = player.GetComponent<PlayerManager>();
        disableTimer = 5;
        damage = 8;
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void Update()
    {
        Vector2 position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector2 dir = new Vector2(direction.x, direction.y);
        position +=dir * 180f * Time.deltaTime;
        transform.position = new Vector3(position.x, position.y, 0);
        if(disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
        else
        {
            disableGameObject();
        }
    }

    public void disableGameObject()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            if(hitpoints > 0)
            {
                disableGameObject();
                player_script.changeRad(-damage);
                hitpoints--;
            }
        }

        if(1 << other.gameObject.layer == 1 << 11)
        {
            disableGameObject();
        }
    }
}
