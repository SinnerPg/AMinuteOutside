using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneManager : MonoBehaviour
{
    public List<GameObject> spawners;
    public GameObject endGame, thanks;
    public Animator endGameAnimator, thanksAnimator;
    GameObject cam;
    CameraManager cameraManager;
    FollowUpMinimapManager minimapManager;
    float timer, targetTimer;
    int counter, hordes;
    public static int lastSceneKillCount;
    void Start()
    {
        lastSceneKillCount = 0;
        minimapManager = GameObject.Find("MinimapCamera").GetComponent<FollowUpMinimapManager>();
        GameObject.Find("NavMeshAreaMap").SetActive(false);
        minimapManager.yClamp = new Vector2(-698, 698);
        targetTimer = 5;
    }

    void Update()
    {
        if(cam == null)
        {
            cam = GameObject.Find("Main Camera");
            cameraManager = cam.GetComponent<CameraManager>();
            foreach(Transform transform in cam.transform)
            {
                transform.gameObject.SetActive(false);
            }
            cameraManager.xClamp = new Vector2(-842, 842);
            cameraManager.yClamp = new Vector2(-698, 698);
            minimapManager.xClamp = new Vector2(-842, 842);
        }
        timer += Time.deltaTime;
        if(timer > targetTimer && hordes < 6)
        {
            timer = 0;
            hordes++;
            switch(hordes)
            {
                case 1:
                    while(counter < 6)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(2, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    while(counter < 3)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(0, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    targetTimer = 15;
                    break;
                case 2:
                    while(counter < 6)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(0, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    while(counter < 2)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 2)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(1, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    targetTimer = 30;
                    break;
                case 3:
                    while(counter < 5)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(2, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    while(counter < 5)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 2)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(0, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    while(counter < 2)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 2)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(1, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    targetTimer = 30;
                    break;
                case 4:
                    while(counter < 1)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(3, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    targetTimer = 30;
                    break;
                case 5:
                    while(counter < 2)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(3, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    while(counter < 3)
                    {
                        int random = Random.Range(0, 10);
                        if(random >= 5)
                        {
                            int randomSpawner = Random.Range(0, spawners.Count);
                            EnemyPoolerLastScene.enemyPoolLastSceneInstance.SpawnEnemy(1, spawners[randomSpawner].transform.position);
                            counter++;
                        }
                    }
                    counter = 0;
                    targetTimer = 30;
                    break;
            }
        }
        if(lastSceneKillCount >= 35)
        {
            endGame.SetActive(true);
            if(endGameAnimator.GetCurrentAnimatorStateInfo(0).IsName("horde") && endGameAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                thanks.SetActive(true);
            }
            if(thanksAnimator.GetCurrentAnimatorStateInfo(0).IsName("horde") && thanksAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                backMenu();
            }
        }
    }

    public void backMenu()
    {
        SceneManager.LoadSceneAsync("MENU");
        SoundManager.soundsList = new List<GameObject>();
    }
}
