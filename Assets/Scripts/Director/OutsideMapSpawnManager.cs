using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideMapSpawnManager : MonoBehaviour
{
    bool canSpawn;
    public float dayTimerSpawn, nightTimerSpawn;
    float timer;
    void Start()
    {
        timer = dayTimerSpawn;
        canSpawn = true;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && canSpawn)
        {
            int random = Random.Range(0, 101);
            if(random >= 0 && random <= 60)
            {
                if(GameManager.nKill <= 50)
                {
                    int randomSpawn = Random.Range(0, 101);
                    if(randomSpawn >= 0 && randomSpawn <= 90)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(2, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 91 && randomSpawn <= 100)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(0, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    
                }
                else if(GameManager.nKill <= 100)
                {
                    int randomSpawn = Random.Range(0, 101);
                    if(randomSpawn >= 0 && randomSpawn <= 70)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(2, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 71 && randomSpawn <= 90)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(0, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 91 && randomSpawn <= 99)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(1, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn == 100)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(3, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                }
                else if(GameManager.nKill <= 200)
                {
                    int randomSpawn = Random.Range(0, 101);
                    if(randomSpawn >= 0 && randomSpawn <= 57)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(2, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 58 && randomSpawn <= 77)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(0, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 78 && randomSpawn <= 97)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(1, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 98 && randomSpawn <= 100)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(3, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                }
                else if(GameManager.nKill <= 350)
                {
                    int randomSpawn = Random.Range(0, 101);
                    if(randomSpawn >= 0 && randomSpawn <= 35)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(2, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 36 && randomSpawn <= 65)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(0, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 66 && randomSpawn <= 95)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(1, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 96 && randomSpawn <= 100)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(3, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                }
                else if(GameManager.nKill > 350)
                {
                    int randomSpawn = Random.Range(0, 101);
                    if(randomSpawn >= 0 && randomSpawn <= 20)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(2, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 21 && randomSpawn <= 55)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(0, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 56 && randomSpawn <= 90)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(1, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                    else if(randomSpawn >= 91 && randomSpawn <= 100)
                    {
                        EnemyPooler.enemyPoolInstance.SpawnEnemy(3, new Vector3(transform.position.x, transform.position.y, 0));
                    }
                }
            }
             if(GameManager.dayTime > 6 && GameManager.dayTime < 22)
            {
                timer = dayTimerSpawn;
            }
            else
            {
                timer = nightTimerSpawn;
            }
        }
        else if(timer <= 0 && !canSpawn)
        {
            if(GameManager.dayTime > 6 && GameManager.dayTime < 22)
            {
                timer = dayTimerSpawn;
            }
            else
            {
                timer = nightTimerSpawn;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 13 || 1 << other.gameObject.layer == 1 << 10 || 1 << other.gameObject.layer == 1 << 11)
        {
            canSpawn = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 13 || 1 << other.gameObject.layer == 1 << 10 || 1 << other.gameObject.layer == 1 << 11)
        {
            canSpawn = true;
        }
    }
}
