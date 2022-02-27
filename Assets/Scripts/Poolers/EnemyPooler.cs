using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public static EnemyPooler enemyPoolInstance;
    private bool spawnZombie = true, spawnFasty = true, spawnFatty = true, spawnBoss = true;
    private List<GameObject> zombieList, fastyList, fattyList, bossList;
    [SerializeField]
    SpawnerPrefabsContainer container;
    [SerializeField]
    Transform parent;

    void Awake() {
        enemyPoolInstance = this;
        zombieList = new List<GameObject>();
        fastyList = new List<GameObject>();
        fattyList = new List<GameObject>();
        bossList = new List<GameObject>();
    }
    public GameObject SpawnEnemy(int id, Vector3 position)  //0: fasty, 1: fatty, 2: zombie, 3: boss
    {
        switch(id)
        {
            case 0:
                if(fastyList.Count > 0)
                {
                    for(int i = 0; i < fastyList.Count; i++)
                    {
                        if(!fastyList[i].activeInHierarchy)
                        {
                            fastyList[i].GetComponent<EnemyManagerJobs>().enabled = true;
                            fastyList[i].GetComponent<EnemyManagerJobs>().enableAI();
                            return fastyList[i];
                        }
                    }
                }

                if(spawnFasty)
                {
                    GameObject obst = Instantiate(container.enemies[0], position, Quaternion.identity, parent);
                    fastyList.Add(obst);
                    return obst;
                }
                return null;
            case 1:
                if(fattyList.Count > 0)
                {
                    for(int i = 0; i < fattyList.Count; i++)
                    {
                        if(!fattyList[i].activeInHierarchy)
                        {
                            fattyList[i].GetComponent<EnemyManagerJobs>().enabled = true;
                            fattyList[i].GetComponent<EnemyManagerJobs>().enableAI();
                            return fattyList[i];
                        }
                    }
                }

                if(spawnFatty)
                {
                    GameObject obst = Instantiate(container.enemies[1], position, Quaternion.identity, parent);
                    fattyList.Add(obst);
                    return obst;
                }
                return null;
            case 2:
                if(zombieList.Count > 0)
                {
                    for(int i = 0; i < zombieList.Count; i++)
                    {
                        if(!zombieList[i].activeInHierarchy)
                        {
                            zombieList[i].GetComponent<EnemyManagerJobs>().enabled = true;
                            zombieList[i].GetComponent<EnemyManagerJobs>().enableAI();
                            return zombieList[i];
                        }
                    }
                }

                if(spawnZombie)
                {
                    GameObject obst = Instantiate(container.enemies[2], position, Quaternion.identity, parent);
                    zombieList.Add(obst);
                    return obst;
                }
                return null;
            case 3:
                if(bossList.Count > 0)
                {
                    for(int i = 0; i < bossList.Count; i++)
                    {
                        if(!bossList[i].activeInHierarchy)
                        {
                            bossList[i].GetComponent<EnemyManagerJobs>().enabled = true;
                            bossList[i].GetComponent<EnemyManagerJobs>().enableAI();
                            return bossList[i];
                        }
                    }
                }

                if(spawnFatty)
                {
                    GameObject obst = Instantiate(container.enemies[3], position, Quaternion.identity, parent);
                    bossList.Add(obst);
                    return obst;
                }
                return null;
            default:
                return null;
        }
    }
}
