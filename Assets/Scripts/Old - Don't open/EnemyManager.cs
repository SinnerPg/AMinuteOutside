using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    enum EnemyState
    {
        idle,
        patrol,
        chase,
        attack
    }
    public AIDestinationSetter destinationSetter;
    public AIPath aiPath;
    public Animator animator;
    public SpriteRenderer sprite;
    [Header("Enemy Settings")]
    public float hp;
    public int damageHp;
    public int damageRad;
    public int velocity;
    public float patrolModifier;
    public float idleTime;
    public float animationSpeed;
    public BoxCollider2D m_collider;
    GameObject target;
    bool hasStartedTimer, isDead;
    Vector3 initialPosition;
    float timePassed;
    EnemyState state;
    PlayerManager playerManager;
    void Start()
    {
        aiPath.maxAcceleration = velocity * 0.5f;
        aiPath.maxSpeed = velocity * 0.5f;
        target = TargetsPooler.targetPoolInstance.SpawnTarget();
        target.SetActive(true);
        initialPosition = transform.position;
        timePassed = idleTime;
        target.transform.position = new Vector3(Mathf.Clamp(transform.position.x + Random.Range(-120, 121), -1965, 1965),
        Mathf.Clamp(transform.position.y + Random.Range(-120, 121), -1465, 1465), 
        0);
        destinationSetter.target = target.transform;
        state = EnemyState.patrol;
    }

    void Update()
    {
        //Debug.Log(aiPath.remainingDistance);
        if(!isDead)
        {
            animator.speed = 1;
            switch(state)
            {
                case EnemyState.idle:
                    if(!hasStartedTimer)
                    {
                        animator.Play("idle");
                        target.SetActive(false);
                        target = null;
                        timePassed = idleTime;
                        hasStartedTimer = true;
                        aiPath.enabled = false;
                    }
                    else
                    {
                        timePassed -= Time.deltaTime;
                    }

                    if(timePassed <= 0 && hasStartedTimer)
                    {
                        target = TargetsPooler.targetPoolInstance.SpawnTarget();
                        target.SetActive(true);
                        target.transform.position = new Vector3(Mathf.Clamp(transform.position.x + Random.Range(-120, 121), -1965, 1965),
                        Mathf.Clamp(transform.position.y + Random.Range(-120, 121), -1465, 1465), 
                        0);
                        destinationSetter.target = target.transform;
                        hasStartedTimer = false;
                        state = EnemyState.patrol;
                    }
                    break;
                case EnemyState.patrol:
                    animator.Play("walk");
                    aiPath.enabled = true;
                    if(aiPath.remainingDistance <= 14)
                        state = EnemyState.idle;
                    break;
                case EnemyState.chase:
                    animator.Play("walk");
                    aiPath.enabled = true;
                    aiPath.maxAcceleration = velocity;
                    aiPath.maxSpeed = velocity;
                    if(gameObject.name != "Fatty")
                    {
                        if(aiPath.remainingDistance <= 22)
                        {
                            state = EnemyState.attack;
                        }
                    }
                    else
                    {
                        if(aiPath.remainingDistance <= 42.1f)
                        {
                            state = EnemyState.attack;
                        }
                    }
                    break;
                case EnemyState.attack:
                    aiPath.enabled = false;
                    animator.speed = animationSpeed;
                    if(!playerManager.isDead)
                    {
                        animator.Play("attack");
                        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            state = EnemyState.chase;
                        }
                    }
                    else
                    {
                        animator.Play("idle");
                    }
                    break;
            }
        }
    }
    public void changeHp(float hp, string weapon)
    {
        this.hp += hp;
        StartCoroutine("TakeDamage");

        if(this.hp <= 0 && weapon == "SG")
        {
            isDead = true;
            aiPath.enabled = false;
            sprite.sortingOrder = -2;
            animator.Play("blood");
            m_collider.enabled = false;
            this.enabled = false;
        }
        else if(this.hp <= 0)
        {
            isDead = true;
            aiPath.enabled = false;
            sprite.sortingOrder = -2;
            animator.Play("dead");
            m_collider.enabled = false;
            this.enabled = false;
        }
    }
    IEnumerator TakeDamage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            destinationSetter.target = other.gameObject.transform;
            if(gameObject.name != "Fatty")
            {
                aiPath.endReachedDistance = 30;
                aiPath.slowdownDistance = 60;
            }
            else
            {
                aiPath.endReachedDistance = 42;
                aiPath.slowdownDistance = 40;
            }
            state = EnemyState.chase;
            playerManager = other.gameObject.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            state = EnemyState.idle;
            if(target) destinationSetter.target = target.transform;
            aiPath.endReachedDistance = 13;
            aiPath.slowdownDistance = 20;
            aiPath.maxAcceleration = velocity * patrolModifier;
            aiPath.maxSpeed = velocity * patrolModifier;
        }
    }
}
