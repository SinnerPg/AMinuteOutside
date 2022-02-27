using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyManagerJobs : MonoBehaviour
{
    enum EnemyState
    {
        idle,
        patrol,
        chase,
        attack,
        dead
    }
    [Header("References")]
    public Animator animator;
    public SpriteRenderer sprite;
    public NavMeshAgent agent;
    public GameObject minimapIcon, parent;
    public List<GameObject> dropItems;
    public BoxCollider2D boxCollider;
    [Header("Enemy Settings")]
    public float hp;
    public int damageHp;
    public int damageRad;
    public int velocity;
    public float patrolModifier;
    public float idleTime;
    public float animationSpeed;
    public BoxCollider2D m_collider;
    public bool lastScene;
    //Private variables
    GameObject target, player;
    bool hasStartedTimer, isDead, shooting, canAttack, sgKillCount;
    Vector3 initialPosition;
    float timePassed, timerDead, chaseTime, dangerTime;
    EnemyState state;
    PlayerManager playerManager;
    Transform tornado, danger; //GameObject del "boss"
    
    void Start()
    {
        if(!lastScene) enableAI(); //Setup variabili per abilitare l'IA
        else
        {
            player = GameObject.Find("Protagonista");
            playerManager = player.GetComponent<PlayerManager>();
            enableAILast();
        }
    }

    void Update()
    {
        if(!isDead)
        {
            if(agent.isActiveAndEnabled)
            {
                Vector3 dir = agent.steeringTarget - parent.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                //Gestisco manualmente la rotazione dello sprite verso la direzione dove sta camminando
                parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.time * 100);
            }
            if(!lastScene)
            {
                animator.speed = 1; //Reset velocità animazione dopo l'attacco
                switch(state)
                {
                    case EnemyState.idle:
                        animator.Play("idle");
                        if(!hasStartedTimer) //Se falso, vuol dire che la IA ha raggiunto il target o è tornato in idle dopo un attacco
                        {
                            if(target) target.SetActive(false);
                            target = null;
                            timePassed = idleTime;
                            hasStartedTimer = true;
                            agent.enabled = false;
                        }
                        else
                        {
                            timePassed -= Time.deltaTime;
                        }

                        if(timePassed <= 0 && hasStartedTimer)
                        {
                            target = TargetsPooler.targetPoolInstance.SpawnTarget();
                            target.SetActive(true);
                            target.transform.position = new Vector3(Mathf.Clamp(transform.position.x + Random.Range(-170, 171), -1965, 1965),
                            Mathf.Clamp(transform.position.y + Random.Range(-170, 171), -1465, 1465), 
                            0);
                            agent.enabled = true;
                            agent.SetDestination(target.transform.position);
                            hasStartedTimer = false;
                            state = EnemyState.patrol;
                        }
                        break;
                    case EnemyState.patrol:
                        animator.Play("walk");
                        agent.enabled = true;
                        if(agent.remainingDistance <= 21)
                            state = EnemyState.idle;
                        break;
                    case EnemyState.chase:
                        if(!shooting) //Condizione creato appositamente per eventuali mostri ranged, come il "boss"
                        {
                            animator.Play("walk");
                            agent.enabled = true;
                            if(GameManager.dayTime > 6 && GameManager.dayTime < 22)
                            {
                                agent.speed = velocity;
                                agent.acceleration = velocity * 4;
                            }
                            else
                            {
                                agent.speed = velocity * 1.5f;
                                agent.acceleration = velocity * 6;
                            }
                            agent.SetDestination(player.transform.position);
                            switch(gameObject.name)
                            {
                                case "Fasty":
                                    if(agent.remainingDistance <= 22 && agent.remainingDistance != 0)
                                    {
                                        state = EnemyState.attack;
                                    }
                                    break;
                                case "Fatty":
                                    if(agent.remainingDistance <= 50 && agent.remainingDistance != 0)
                                    {
                                        state = EnemyState.attack;
                                    }
                                    break;
                                case "Zombie":
                                    if(agent.remainingDistance <= 25 && agent.remainingDistance != 0)
                                    {
                                        state = EnemyState.attack;
                                    }
                                    break;
                                case "Boss":
                                    if(agent.remainingDistance <= 40 && agent.remainingDistance != 0)
                                    {
                                        state = EnemyState.attack;
                                    }
                                    break;
                            }
                        }
                        if(gameObject.name == "Boss") chaseTime += Time.deltaTime; //Se sei il boss, calcoli il tempo di inseguita così da attaccare ranged
                        if(chaseTime >= 1.5f)
                        {
                            shooting = true;
                            agent.enabled = false;
                            animator.Play("ranged");
                            SoundManager.PlaySound(SoundManager.Sound.BossShot);
                            if(animator.GetCurrentAnimatorStateInfo(0).IsName("ranged") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                            {
                                chaseTime = 0f;
                                agent.enabled = true;
                                shooting = false;
                            }
                        }
                        break;
                    case EnemyState.attack:
                        agent.enabled = false;
                        animator.speed = animationSpeed;
                        if(!playerManager.isDead)
                        {
                            if(gameObject.name != "Boss")
                            {
                                animator.Play("attack");
                                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                                {
                                    agent.enabled = true;
                                    agent.SetDestination(player.transform.position);
                                    state = EnemyState.chase;
                                }
                            }
                            else
                            {
                                if(tornado == null) tornado = gameObject.transform.Find("Tornado");
                                if(danger == null) danger = gameObject.transform.Find("Danger");
                                danger.gameObject.SetActive(true);
                                dangerTime += Time.deltaTime;
                                if(dangerTime >= .5f)
                                {
                                    tornado.gameObject.SetActive(true);
                                    danger.gameObject.SetActive(false);
                                    animator.Play("attack");
                                    SoundManager.PlaySound(SoundManager.Sound.MeleeZombie);
                                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                                    {
                                        tornado.gameObject.SetActive(false);
                                        hasStartedTimer = false;
                                        dangerTime = 0;
                                        chaseTime = 0;
                                        if(playerManager)
                                        {
                                            state = EnemyState.chase;
                                        }
                                        else
                                        {
                                            state = EnemyState.idle;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                            {
                                animator.Play("idle");
                                if(tornado) tornado.gameObject.SetActive(false);
                            }
                        }
                        break;
                    case EnemyState.dead:
                        return;
                }
            }
            else
            {
                animator.speed = 1; //Reset velocità animazione dopo l'attacco
                switch(state)
                {
                    case EnemyState.chase:
                        if(!shooting) //Condizione creato appositamente per eventuali mostri ranged, come il "boss"
                        {
                            animator.Play("walk");
                            agent.enabled = true;
                            if(GameManager.dayTime > 6 && GameManager.dayTime < 22)
                            {
                                agent.speed = velocity;
                                agent.acceleration = velocity * 4;
                            }
                            else
                            {
                                agent.speed = velocity * 1.5f;
                                agent.acceleration = velocity * 6;
                            }
                            agent.SetDestination(player.transform.position);
                            if(canAttack)
                            {
                                switch(gameObject.name)
                                {
                                    case "Fasty":
                                        if(agent.remainingDistance <= 22 && agent.remainingDistance != 0)
                                        {
                                            state = EnemyState.attack;
                                        }
                                        break;
                                    case "Fatty":
                                        if(agent.remainingDistance <= 50 && agent.remainingDistance != 0)
                                        {
                                            state = EnemyState.attack;
                                        }
                                        break;
                                    case "Zombie":
                                        if(agent.remainingDistance <= 25 && agent.remainingDistance != 0)
                                        {
                                            state = EnemyState.attack;
                                        }
                                        break;
                                    case "Boss":
                                        if(agent.remainingDistance <= 40 && agent.remainingDistance != 0)
                                        {
                                            state = EnemyState.attack;
                                        }
                                        break;
                                }
                            }
                        }
                        if(gameObject.name == "Boss") chaseTime += Time.deltaTime; //Se sei il boss, calcoli il tempo di inseguita così da attaccare ranged
                        if(chaseTime >= 1.5f)
                        {
                            shooting = true;
                            agent.enabled = false;
                            animator.Play("ranged");
                            SoundManager.PlaySound(SoundManager.Sound.BossShot);
                            if(animator.GetCurrentAnimatorStateInfo(0).IsName("ranged") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                            {
                                chaseTime = 0f;
                                agent.enabled = true;
                                shooting = false;
                            }   
                        }
                        break;
                    case EnemyState.attack:
                        agent.enabled = false;
                        animator.speed = animationSpeed;
                        if(!playerManager.isDead)
                        {
                            if(gameObject.name != "Boss")
                            {
                                animator.Play("attack");
                                if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                                {
                                    agent.enabled = true;
                                    agent.SetDestination(player.transform.position);
                                    state = EnemyState.chase;
                                }
                            }
                            else
                            {
                                if(tornado == null) tornado = gameObject.transform.Find("Tornado");
                                if(danger == null) danger = gameObject.transform.Find("Danger");
                                danger.gameObject.SetActive(true);
                                dangerTime += Time.deltaTime;
                                if(dangerTime >= .5f)
                                {
                                    tornado.gameObject.SetActive(true);
                                    danger.gameObject.SetActive(false);
                                    animator.Play("attack");
                                    SoundManager.PlaySound(SoundManager.Sound.MeleeZombie);
                                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                                    {
                                        tornado.gameObject.SetActive(false);
                                        hasStartedTimer = false;
                                        state = EnemyState.chase;
                                        dangerTime = 0;
                                        chaseTime = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                            {
                                animator.Play("idle");
                                if(tornado) tornado.gameObject.SetActive(false);
                            }
                        }
                        break;
                    case EnemyState.dead:
                        GameManager.nKill++;
                        if(lastScene)
                        {
                            LastSceneManager.lastSceneKillCount++;
                        }
                        return;
                }
            }
        }
        else
        {
            timerDead += Time.deltaTime;
            if(timerDead >= 120)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void enableAI()
    {
        minimapIcon.SetActive(true);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = velocity * patrolModifier;
        agent.acceleration = velocity * patrolModifier;
        target = TargetsPooler.targetPoolInstance.SpawnTarget();
        target.SetActive(true);
        initialPosition = transform.position;
        timePassed = idleTime;
        target.transform.position = new Vector3(Mathf.Clamp(transform.position.x + Random.Range(-170, 171), -1965, 1965),
        Mathf.Clamp(transform.position.y + Random.Range(-170, 171), -1465, 1465), 
        0);
        agent.enabled = true;
        m_collider.enabled = true;
        boxCollider.enabled = true;
        agent.SetDestination(target.transform.position);
        state = EnemyState.patrol;
    }

    public void enableAILast()
    {
        minimapIcon.SetActive(true);
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = true;
        m_collider.enabled = true;
        boxCollider.enabled = true;
        agent.SetDestination(player.transform.position);
        state = EnemyState.chase;
    }

    public void changeHp(float hp, string weapon)
    {
        this.hp += hp;
        StartCoroutine("TakeDamage");

        if(this.hp <= 0 && weapon == "SG")
        {
            state = EnemyState.dead;
            isDead = true;
            agent.enabled = false;
            this.GetComponent<EnemyManagerJobs>().enabled = false;
            sprite.sortingOrder = -3;
            boxCollider.enabled = false;
            animator.Play("blood");
            m_collider.enabled = false;
            minimapIcon.SetActive(false);
            if(target) target.SetActive(false);
            target = null;
            if(gameObject.name == "Boss") danger.gameObject.SetActive(false);
            if(!sgKillCount)
            {           //Più colpi possono colpire nello stesso momento, con la booleana mi assicuro che venga calcolato una volta
                sgKillCount = true;
                GameManager.nKill++;
                if(lastScene)
                {
                    LastSceneManager.lastSceneKillCount++;
                }
            }
            this.enabled = false;
        }
        else if(this.hp <= 0)
        {
            isDead = true;
            agent.enabled = false;
            this.GetComponent<EnemyManagerJobs>().enabled = false;
            sprite.sortingOrder = -3;//Forse 2?
            boxCollider.enabled = false;
            animator.Play("dead");
            m_collider.enabled = false;
            GameManager.nKill++;
            minimapIcon.SetActive(false);
            state = EnemyState.dead;
            if(target) target.SetActive(false);
            target = null;
            if(gameObject.name == "Boss") danger.gameObject.SetActive(false);
            if(lastScene)
            {
                LastSceneManager.lastSceneKillCount++;
            }
            this.enabled = false;
        }
    }

    public void dropItem()
    {
        int randomSpawn = Random.Range(0, 101);
        switch(gameObject.name)
        {
            case "Fasty":
                if(randomSpawn >= 0 && randomSpawn <= 75)
                {
                    int randomSecondSpawn = Random.Range(0,51);
                    int randomItem = Random.Range(0, 2);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 5)
                    {
                        GameObject spawned = Instantiate(dropItems[0], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 6 && randomSecondSpawn <= 50)
                    {
                        GameObject spawned = Instantiate(dropItems[1], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
                break;
            case "Fatty":
                if(randomSpawn >= 0 && randomSpawn <= 75)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    int randomItem = Random.Range(0, 2);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 10)
                    {
                        GameObject spawned = Instantiate(dropItems[0], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 11 && randomSecondSpawn <= 25)
                    {
                        GameObject spawned = Instantiate(dropItems[1], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 26 && randomSecondSpawn <= 40)
                    {
                        GameObject spawned = Instantiate(dropItems[2], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 41 && randomSecondSpawn <= 100)
                    {
                        GameObject spawned = Instantiate(dropItems[3], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
                break;
            case "Zombie":
                if(randomSpawn >= 0 && randomSpawn <= 75)
                {
                    int randomSecondSpawn = Random.Range(0,51);
                    int randomItem = Random.Range(0, 2);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 5)
                    {
                        GameObject spawned = Instantiate(dropItems[0], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 6 && randomSecondSpawn <= 50)
                    {
                        GameObject spawned = Instantiate(dropItems[1], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
                break;
            case "Boss":
                if(randomSpawn >= 0 && randomSpawn <= 75)
                {
                    int randomSecondSpawn = Random.Range(0,101);
                    int randomItem = Random.Range(0, 2);
                    if(randomSecondSpawn >= 0 && randomSecondSpawn <= 10)
                    {
                        GameObject spawned = Instantiate(dropItems[0], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 11 && randomSecondSpawn <= 35)
                    {
                        GameObject spawned = Instantiate(dropItems[1], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 36 && randomSecondSpawn <= 40)
                    {
                        GameObject spawned = Instantiate(dropItems[2], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 41 && randomSecondSpawn <= 50)
                    {
                        GameObject spawned = Instantiate(dropItems[3], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    else if(randomSecondSpawn >= 51 && randomSecondSpawn <= 100)
                    {
                        GameObject spawned = Instantiate(dropItems[4], transform.position, Quaternion.identity, transform);
                        spawned.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                        spawned.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
                break;
        }
    }
    IEnumerator TakeDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    public void throwProjectile()
    {
        InventoryManager inventory = GameObject.Find("GameDirector").GetComponent<InventoryManager>();
        Instantiate(inventory.projectiles[2], transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            if(!lastScene)
            {
                state = EnemyState.chase;
                player = other.gameObject;
                playerManager = other.gameObject.GetComponent<PlayerManager>();
            }
            else
            {
                canAttack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            if(!lastScene)
            {
                state = EnemyState.idle;
                hasStartedTimer = false;
                agent.speed = velocity * patrolModifier;
                agent.acceleration = velocity * patrolModifier;
                playerManager = null;
                player = null;
            }
            else
            {
                canAttack = false;
            }
        }
    }
}
