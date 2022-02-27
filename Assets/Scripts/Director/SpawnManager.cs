using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<Transform> cityMarketSpawners = new List<Transform>(), ruralMarketSpawners= new List<Transform>(), 
    gasStationSpawners = new List<Transform>(), ammoStationSpawners = new List<Transform>(), crashedCarsSpawners = new List<Transform>(),
    terraSpawners = new List<Transform>(), enemySpawner = new List<Transform>();
    SpawnerPrefabsContainer pickupsContainer;
    public Transform cityMarket, ruralMarket, gasStation, ammoStation, crashedCars, terra, enemy;
    public string sceneName;
    Transform gameObjectContainer;
    void Awake()
    {
        pickupsContainer = GameObject.Find("GameDirector").GetComponent<SpawnerPrefabsContainer>();
        if(cityMarket != null)
        {
            foreach(Transform go in cityMarket.transform)
            {
                cityMarketSpawners.Add(go);
            }
        }
        if(ruralMarket != null)
        {
            foreach(Transform go in ruralMarket.transform)
            {
                ruralMarketSpawners.Add(go);
            }
        }
        if(gasStation != null)
        {
            foreach(Transform go in gasStation.transform)
            {
                gasStationSpawners.Add(go);
            }
        }
        if(ammoStation != null)
        {
            foreach(Transform go in ammoStation.transform)
            {
                ammoStationSpawners.Add(go);
            }
        }
        if(crashedCars != null)
        {
            foreach(Transform go in crashedCars.transform)
            {
                crashedCarsSpawners.Add(go);
            }
        }
        if(terra != null)
        {
            foreach(Transform go in terra.transform)
            {
                terraSpawners.Add(go);
            }
        }
        if(enemy != null)
        {
            foreach(Transform go in enemy.transform)
            {
                enemySpawner.Add(go);
            }
        }
    }

    void Start()
    {
        if(!ChunkManager.sceneData[sceneName])
        {
            gameObjectContainer = GameObject.Find("GameObjectContainer").transform;
            spawnObjects();
            ChunkManager.sceneData[sceneName] = true;
        }
    }

    void spawnObjects()
    {
        if(cityMarketSpawners.Count > 0)
        {
            foreach(Transform go in cityMarketSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 60 && !InventoryManager.Torcia_obtained)
                    {
                        GameObject spawned = Instantiate(pickupsContainer.cityMarketSpecial[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                    }
                    else if(randomSecondSpawn >= 31 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 20)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 21 && randomItemSpawn <= 40)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 41 && randomItemSpawn <= 50)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 51 && randomItemSpawn <= 60)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[3], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 61 && randomItemSpawn <= 75)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[4], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 76 && randomItemSpawn <= 85)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[5], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 86 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.cityMarketPickups[6], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                    }
                }
            }
        }

        if(ruralMarketSpawners.Count > 0)
        {
            foreach(Transform go in ruralMarketSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    GameObject spawned;
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 60 && !InventoryManager.Torcia_obtained)
                    {
                        spawned = Instantiate(pickupsContainer.ruralMarketSpecial[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                    }
                    else if(randomSecondSpawn >= 31 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 30)
                        {
                            spawned = Instantiate(pickupsContainer.ruralMarketPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 31 && randomItemSpawn <= 55)
                        {
                            spawned = Instantiate(pickupsContainer.ruralMarketPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 56 && randomItemSpawn <= 65)
                        {
                            spawned = Instantiate(pickupsContainer.ruralMarketPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 66 && randomItemSpawn <= 85)
                        {
                            spawned = Instantiate(pickupsContainer.ruralMarketPickups[3], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 86 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.ruralMarketPickups[4], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                    }
                }
            }
        }

        if(gasStationSpawners.Count > 0)
        {
            foreach(Transform go in gasStationSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    int randomItem = Random.Range(0, 101);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 50)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 90 && !InventoryManager.Torcia_obtained)
                        {
                            spawned = Instantiate(pickupsContainer.gasStationSpecial[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                        }
                        else if(randomItemSpawn >= 61 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.gasStationPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                        }
                    }
                    else if(randomSecondSpawn >= 51 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 20)
                        {
                            spawned = Instantiate(pickupsContainer.gasStationPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 21 && randomItemSpawn <= 60)
                        {
                            spawned = Instantiate(pickupsContainer.gasStationPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 61 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.gasStationPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                    }
                }
            }
        }

        if(ammoStationSpawners.Count > 0)
        {
            foreach(Transform go in ammoStationSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    int randomItem = Random.Range(0, 101);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 50)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 10 && !InventoryManager.Torcia_obtained)
                        {
                            spawned = Instantiate(pickupsContainer.ammoStationSpecial[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                        }
                        else if(randomItemSpawn >= 0 && randomItemSpawn <= 80)
                        {
                            spawned = Instantiate(pickupsContainer.ammoStationSpecial[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                        }
                        else if(randomItemSpawn >= 81 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.ammoStationSpecial[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                        }
                    }
                    else if(randomSecondSpawn >= 51 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 65)
                        {
                            spawned = Instantiate(pickupsContainer.ammoStationPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 66 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.ammoStationPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                    }
                }
            }
        }

        if(crashedCarsSpawners.Count > 0)
        {
            foreach(Transform go in crashedCarsSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomItem = Random.Range(0, 101);
                    int randomSecondSpawn = Random.Range(0,101);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 25)
                        {
                            spawned = Instantiate(pickupsContainer.crashedCarsPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 26 && randomItemSpawn <= 50)
                        {
                            spawned = Instantiate(pickupsContainer.crashedCarsPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 51 && randomItemSpawn <= 65)
                        {
                            spawned = Instantiate(pickupsContainer.crashedCarsPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 66 && randomItemSpawn <= 80)
                        {
                            spawned = Instantiate(pickupsContainer.crashedCarsPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 81 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.crashedCarsPickups[3], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        
                        
                    }
                }
            }
        }

        if(terraSpawners.Count > 0)
        {
            foreach(Transform go in terraSpawners)
            {
                int randomSpawn = Random.Range(0, 101);
                if(randomSpawn >= 0 && randomSpawn <= 85)
                {
                    int randomItem = Random.Range(0, 101);
                    int randomSecondSpawn = Random.Range(0,101);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 100)
                    {
                        int randomItemSpawn = Random.Range(0, 101);
                        int randomInv = Random.Range(0, 101);
                        GameObject spawned;
                        if(randomItemSpawn >= 0 && randomItemSpawn <= 80)
                        {
                            spawned = Instantiate(pickupsContainer.terraPickups[0], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 81 && randomItemSpawn <= 95)
                        {
                            spawned = Instantiate(pickupsContainer.terraPickups[1], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                        else if(randomItemSpawn >= 96 && randomItemSpawn <= 100)
                        {
                            spawned = Instantiate(pickupsContainer.terraPickups[2], go.transform.position, Quaternion.identity, gameObjectContainer);
                            if(randomInv >= 0 && randomInv <= 20)
                            {
                                spawned.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                            }
                        }
                    }
                }
            }
        }
        if(enemySpawner.Count > 0)
        {
            foreach(Transform go in enemySpawner)
            {
                int randomSpawn = Random.Range(0, 101);
                if(GameManager.nKill <= 50)
                {
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
        }
    }
}
