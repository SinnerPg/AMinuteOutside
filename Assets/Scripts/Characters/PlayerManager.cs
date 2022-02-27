using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    enum PlayerState
    {
        idle,
        walking,
        idle_aim,
        walking_aim,
        melee_atk,
        dashing
    }
    [Header("Player Levels")]
    public int levelHp;
    public int levelRad;
    public int levelBag;
    public int levelSpeed;
    public int levelJetpack;
    [HideInInspector]public bool isDead;
    [HideInInspector]public bool isShelter;
    [Header("Player Settings")]
    [Range(0, 1)]
    public float rotationSpeed;
    public float speed;
    public int hp;
    public int rad;
    public float dashTimer;
    public List<int> hpLevelValues;
    public List<int> radLevelValues;
    public List<int> bagLevelValues;
    public List<int> speedLevelValues;
    public List<float> jetpackLevel;
    [Header("References")]
    public InputManager inputManager;
    public ReloadManager reloadManager;
    public InventoryManager inventoryManager;
    public InventoryCanvasManager inventoryCanvasManager;
    public ChunkManager chunkManager;
    public Animator bodyAnimator, armsAnimator, smgMuzzleAnimator, sgMuzzleAnimator;
    public List<RuntimeAnimatorController> animators; //0: melee, 1:smg, 2: sg, 3: torcia, 4:arms
    public SpriteRenderer body, armsSprite;
    public LineRenderer aimLine;
    public GameObject bloodPool, armsGO, smgMuzzle, sgMuzzle, torcia, startProjectile;
    public GameManager gameManager;
    [HideInInspector]
    public bool cantChange; //Serve per non cambiare arma mentre si usa l'attacco melee
    Rigidbody2D m_rb;
    bool m_shooting, m_damaged, m_dashing, m_loading, m_slowed;
    int m_currentWeapon;
    Vector2 velocity;
    PlayerState state = PlayerState.idle;
    Vector3 mousePos;
    float m_dashTimer, m_timer, initialSpeed;

    void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_currentWeapon = 0;
        //Lettura dati da salvataggio se esistente, altrimenti valore di default
        hp = hpLevelValues[GameData.hpLevelPlayer];
        levelHp = GameData.hpLevelPlayer;
        rad = radLevelValues[GameData.radLevelPlayer];
        levelRad = GameData.radLevelPlayer;
        speed = speedLevelValues[GameData.speedLevelPlayer];
        levelSpeed = GameData.speedLevelPlayer;
        levelBag = GameData.bagLevelPlayer;
        levelJetpack =  GameData.jetpackLevelPlayer;
        initialSpeed = speed;
        if(GameData.day == 5) transform.position = new Vector3(0, -310, 0);
    }
    void Update()
    {
        if(!isDead)
        {
            //Se il personaggio non è morto, può compiere tutte le azioni, altrimenti parte un animazione e vengono bloccati input e velocità
            if(!isShelter)
            {
                PlayersInput();
                Animations();
                m_timer += Time.deltaTime;
                if(m_timer > 1)
                {
                    if(rad > 0)
                    {
                        rad--;
                    }
                    m_timer = 0;
                    if(rad <= 0)
                    {
                        changeHp(-1);
                    }
                }
                if(m_loading)
                {
                    Invoke("stopLoading", 0.5f);
                }
            }
        }
        else
        {
            if(bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("dying") && bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                bodyAnimator.Play("dead");
            }
            if(bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("dead") && bodyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                gameManager.deathScreen();
            }
        }
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            if(!isShelter)
            {
                Movement();
            }
            else
            {
                m_rb.velocity = Vector2.zero;
            }
        }
    }

    void PlayersInput()
    {
        #region PLAYERS_INPUT
        //Weapons
        if(inputManager.isAiming && !m_dashing && inventoryManager.equipWeapon < 4)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 toTargetVector = new Vector3(mousePos.x, mousePos.y, 0.0f) - transform.position;
            float zRotation = Mathf.Atan2(toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            if(m_currentWeapon != 3)
            {
                aimLine.enabled = true;
                aimLine.SetPosition(0, smgMuzzle.transform.position);
                aimLine.SetPosition(1, mousePos);
            }
            else
            {
                if(inventoryManager.inventory[3].stack > 0)
                {
                    torcia.SetActive(true);
                    inventoryManager.inventory[3].stack -= Time.deltaTime;
                }
                else
                {
                    torcia.SetActive(false);
                    int position = -1;
                    bool search = true;
                    while(position < inventoryManager.bag.Count - 1 && search)
                    {
                        position++;
                        if(inventoryManager.bag[position] != null)
                        {
                            if(inventoryManager.bag[position].id == 11)
                            {
                                search = false;
                                InventoryManager.addTorcia = true;
                                inventoryManager.bag[position].stack--;
                                if(inventoryManager.bag[position].stack == 0)
                                {
                                    Destroy(inventoryManager.bag[position].gameObject);
                                    inventoryCanvasManager.removeBag(position);
                                }
                            }
                        }
                    }
                }
            }

            if(inputManager.isShooting)
            {
                switch(inventoryManager.equipWeapon)
                {
                    case 0:
                        cantChange = true;
                        state = PlayerState.melee_atk;
                        break;
                    case 1:
                        if(reloadManager.canShootSMG && InventoryManager.SMG_obtained && InventoryManager.SMG.getAmmo() > 0)
                        {
                            GameObject bullet = ProjectilesPooler.projectilePoolInstance.SpawnSMGProjectile();
                            Vector3 direction = transform.eulerAngles;
                            bullet.GetComponent<ProjectileManager>().Shot(direction, "SMG");
                            if (bullet != null)
                            {
                                bullet.transform.position = startProjectile.transform.position;
                                bullet.SetActive(true);
                            }
                            smgMuzzle.SetActive(true);
                            smgMuzzleAnimator.Play("muzzle");
                            SoundManager.PlaySound(SoundManager.Sound.SMGShot);
                            reloadManager.canShootSMG = false;
                            inventoryCanvasManager.startReload(1, 3.03f);
                            InventoryManager.SMG.setAmmo(-1);
                        }
                        break;
                    case 2:
                        if(reloadManager.canShootSG && InventoryManager.SG_obtained && InventoryManager.SG.getAmmo() > 0)
                        {
                            for(int i = 0; i < 5; i++)
                            {
                                GameObject bullet = ProjectilesPooler.projectilePoolInstance.SpawnSGProjectile();
                                Vector3 direction = new Vector3(0, 0, (transform.eulerAngles.z - 10) + (i * 5));
                                bullet.GetComponent<ProjectileManager>().Shot(direction, "SG");
                                bullet.transform.position = startProjectile.transform.position;
                                bullet.SetActive(true);
                            }
                            sgMuzzle.SetActive(true);
                            sgMuzzleAnimator.Play("muzzle");
                            SoundManager.PlaySound(SoundManager.Sound.SGShot);
                            reloadManager.canShootSG = false;
                            inventoryCanvasManager.startReload(2, 0.33f);
                            InventoryManager.SG.setAmmo(-5);
                        }
                        break;
                }
            }
        }
        else
        {
            aimLine.enabled = false;
            torcia.SetActive(false);
        }

        //Objects
        if(inputManager.isShooting && !m_dashing)
        {
            switch(inventoryManager.equipWeapon)
            {
                case 4:
                    if(inventoryManager.inventory[4].stack > 0)
                    {
                        if(hp < hpLevelValues[levelHp])
                        {
                            changeHp(10);
                            inventoryManager.inventory[4].stack--;
                            SoundManager.PlaySound(SoundManager.Sound.Bandage);
                        }
                    }
                    break;
                case 5:
                    if(inventoryManager.inventory[5].stack > 0)
                    {
                        if(hp < hpLevelValues[levelHp])
                        {
                            changeHp((int)Mathf.Round(hpLevelValues[levelHp] * 0.5f));
                            inventoryManager.inventory[5].stack--;  
                        }
                    }
                    break;
                case 6:
                    if(inventoryManager.inventory[6].stack > 0)
                    {
                        if(rad < radLevelValues[levelRad])
                        {
                            changeRad(6);
                            inventoryManager.inventory[6].stack--;
                            SoundManager.PlaySound(SoundManager.Sound.Pollo);
                        } 
                    }
                    break;
            }
        }

        //Controllo per accertarsi che non si può dashare mentre si sta mirando o sparando
        if(!inputManager.isAiming && !inputManager.isShooting)
        {
            if(inputManager.isDashing && !m_dashing)
            {
                int position = -1;
                bool search = true;
                while(position < inventoryManager.bag.Count - 1 && search)
                {
                    position++;
                    if(inventoryManager.bag[position] != null)
                    {
                        if(inventoryManager.bag[position].id == 12)
                        {
                            search = false;
                            m_dashing = true;
                            m_dashTimer = dashTimer;
                            speed = speed * jetpackLevel[levelJetpack];
                            armsGO.SetActive(false);
                            state = PlayerState.dashing;
                            inventoryManager.bag[position].stack--;
                            inventoryCanvasManager.startReload(0, 0.5f);
                            SoundManager.PlaySound(SoundManager.Sound.Dash);
                            if(inventoryManager.bag[position].stack == 0)
                            {
                                Destroy(inventoryManager.bag[position].gameObject);
                                inventoryCanvasManager.removeBag(position);
                            }
                        }
                    }
                }
            }
        }

        if(!m_dashing && !m_slowed)
        {
            speed = speedLevelValues[levelSpeed];
        }
        #endregion
    }

    void Animations()
    {
        switch(state)
        {
            case PlayerState.idle:
                bodyAnimator.Play("idle");
                armsAnimator.Play("idle");
                break;
            case PlayerState.idle_aim:
                bodyAnimator.Play("idle");
                armsAnimator.Play("aim");
                break;
            case PlayerState.walking:
                bodyAnimator.Play("walk");
                armsAnimator.Play("walk");
                break;
            case PlayerState.walking_aim:
                bodyAnimator.Play("walk");
                armsAnimator.Play("aim");
                break;
            case PlayerState.melee_atk:
                armsAnimator.Play("attack");
                if(armsAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack") && armsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
                {
                    SoundManager.PlaySound(SoundManager.Sound.Melee);
                }
                if(armsAnimator.GetCurrentAnimatorStateInfo(0).IsName("attack") && armsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    if(inputManager.isShooting)
                    {
                        armsAnimator.Play("attack", 0, 0.0f);
                        //Fix per lo state melee, in quanto tenendelo premuto, ci si poteva muovere attaccando.
                    }
                    else
                    {
                        state = PlayerState.idle;
                        cantChange = false;
                    }
                }
                break;
            case PlayerState.dashing:
                bodyAnimator.Play("idle_jetpack");
                m_dashTimer -= Time.deltaTime;
                if(m_dashTimer <= 0)
                {
                    state = PlayerState.idle;
                    armsGO.SetActive(true);
                    m_dashing = false;
                }
                break;
        }

        m_currentWeapon = 4;
        switch(inventoryManager.equipWeapon)
        {
            case 0:
                m_currentWeapon = 0;
                break;
            case 1:
                if(InventoryManager.SMG_obtained) m_currentWeapon = 1;
                break;
            case 2:
                if(InventoryManager.SG_obtained) m_currentWeapon = 2;
                break;
            case 3:
                if(InventoryManager.Torcia_obtained) m_currentWeapon = 3;
                break;
        }
        armsAnimator.runtimeAnimatorController = animators[m_currentWeapon];

        if(smgMuzzleAnimator.GetCurrentAnimatorStateInfo(0).IsName("muzzle") && smgMuzzleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            smgMuzzle.SetActive(false);
        }
        if(sgMuzzleAnimator.GetCurrentAnimatorStateInfo(0).IsName("muzzle") && sgMuzzleAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            sgMuzzle.SetActive(false);
        }
    }

    void Movement()
    {
        #region FIXED_MOVEMENT
        velocity = Vector2.zero;
        if(inputManager.isAiming && state != PlayerState.melee_atk && state != PlayerState.dashing)
        {
            state = PlayerState.idle_aim;
        }
        else if(!inputManager.isAiming && state != PlayerState.melee_atk && state != PlayerState.dashing)
        {
            state = PlayerState.idle;
        }
        if(state != PlayerState.melee_atk)
        {
            if(inputManager.walkLeft || inputManager.walkRight || inputManager.walkUp || inputManager.walkDown)
            {
                if(inputManager.isAiming && !m_dashing)
                {
                    state = PlayerState.walking_aim;
                }
                else if(!inputManager.isAiming && !m_dashing)
                {
                    state = PlayerState.walking;
                }

                if(inputManager.walkLeft)
                {
                    if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 180), rotationSpeed);
                    velocity.x = -speed * Time.deltaTime;
                }

                if(inputManager.walkRight)
                {
                    if(transform.eulerAngles.z >= 190)
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 360), rotationSpeed);
                        velocity.x = speed * Time.deltaTime;
                    }
                    else
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 0), rotationSpeed);
                        velocity.x = speed * Time.deltaTime;
                    }
                }

                if(inputManager.walkUp)
                {
                    if(transform.eulerAngles.z >= 270)
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 450), rotationSpeed);
                        velocity.y = speed * Time.deltaTime;
                    }
                    else
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 90), rotationSpeed);
                        velocity.y = speed * Time.deltaTime;
                    }
                }

                if(inputManager.walkDown)
                {
                    if(transform.eulerAngles.z >= 90)
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, 270), rotationSpeed);
                        velocity.y = -speed * Time.deltaTime;
                    }
                    else
                    {
                        if(!inputManager.isAiming) transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, -90), rotationSpeed);
                        velocity.y = -speed * Time.deltaTime;
                    }
                }
            }
        }

        if((inputManager.walkLeft && inputManager.walkRight) || (inputManager.walkUp && inputManager.walkDown))
        {
            velocity = Vector2.zero;
            if(state != PlayerState.dashing) state = PlayerState.idle;
        }

        if((inputManager.walkLeft && inputManager.walkUp) || (inputManager.walkRight && inputManager.walkUp) || (inputManager.walkLeft && inputManager.walkDown) || 
        (inputManager.walkRight && inputManager.walkDown))
        {
            velocity = velocity * 0.94f; //Prevenire il boost di velocità in obliquo
        }

        if(!velocity.Equals(Vector2.zero) && state != PlayerState.dashing)
        {
            SoundManager.PlaySound(SoundManager.Sound.Walk);
        }

        if(GameData.day < 5) transform.position = new Vector3(Mathf.Clamp(Mathf.Round(transform.position.x), -1980, 1980), Mathf.Clamp(Mathf.Round(transform.position.y), -1500, 1500), 0);
        else transform.position = new Vector3(Mathf.Clamp(Mathf.Round(transform.position.x), -1184, 1184), Mathf.Clamp(Mathf.Round(transform.position.y), -889, 297), 0);

        m_rb.velocity = velocity;
        #endregion
    }

    public void changeHp(int hp)
    {
        if(hp > 0)
        {
            this.hp += hp;
            if(this.hp >= hpLevelValues[levelHp])
            {
                this.hp = hpLevelValues[levelHp];
            }
        }
        else if(!m_damaged)
        {
            this.hp += hp;
            if(this.hp <= 0)
            {
                isDead = true;
                armsGO.SetActive(false);
                bodyAnimator.Play("dying");
                aimLine.enabled = false;
                m_rb.velocity = Vector2.zero;
                SoundManager.PlaySound(SoundManager.Sound.Death);
            }
            else if(hp < 0)
            {
                m_damaged = true;
                StartCoroutine("TakeDamage");
                if(this.hp > 0)
                {
                    if(Random.Range(0,2) == 0)
                    {
                        SoundManager.PlaySound(SoundManager.Sound.Hurt1);
                    }
                    else
                    {
                        SoundManager.PlaySound(SoundManager.Sound.Hurt2);
                    }
                }
                else
                {
                    SoundManager.PlaySound(SoundManager.Sound.Heal);
                }
            }
        }
    }
    public void changeRad(int rad) //Questa funzione, nel prototipo, è richiamata solo quando si viene colpiti dal proiettile del boss
    {
        this.rad += rad;
        if(this.rad > radLevelValues[levelRad])
        {
            this.rad = radLevelValues[levelRad];
        }
        if(this.rad < 0)
        {
            this.rad = 0;
        }
        if(rad < 0)
        {
            if(!m_slowed)
            {
                StartCoroutine("TakeRadDamage");
                m_slowed = true;
                speed = initialSpeed - 500;
            }
            else
            {
                StopCoroutine("TakeRadDamage");
                StartCoroutine("TakeRadDamage");
                m_slowed = true;
                speed = initialSpeed - 500;
            }
        }
    }
    public void updateStats()
    {
        //Uscito dallo shelter, aggiorniamo i valori in base agli eventuali nuovi livelli
        hp = hpLevelValues[levelHp];
        rad = radLevelValues[levelRad];
        speed = speedLevelValues[levelSpeed];
    }
    IEnumerator TakeDamage()
    {
        //Flash di rosso per i danni
        body.color = Color.red;
        armsSprite.color = Color.red;
        bloodPool.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        body.color = Color.white;
        armsSprite.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        m_damaged = false;
    }
    IEnumerator TakeRadDamage()
    {
        //Flash di verde per i danni radioattivi
        body.color = Color.green;
        armsSprite.color = Color.green;
        yield return new WaitForSeconds(0.3f);
        body.color = Color.white;
        armsSprite.color = Color.white;
        yield return new WaitForSeconds(4f);
        m_slowed = false;
    }

    void stopLoading()
    {
        m_loading = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
        //Danno al contatto con nemici
        if(1 << other.gameObject.layer == 1 << 7 && !isDead)
        {
            changeHp(-other.GetComponentInChildren<EnemyManagerJobs>().damageHp);
        }

        //Caricamento chuck
        if(1 << other.gameObject.layer == 1 << 12)
        {
            if(!m_loading)
            { 
                m_loading = true;
                chunkManager.checkScenes(other.gameObject.name);
            }
        }

        //Danno al contatto con colpo speciale boss
        if(1 << other.gameObject.layer == 1 << 16 && !isDead)
        {
            changeHp(-other.GetComponentInParent<EnemyManagerJobs>().damageHp);
        }
    }
}
