using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemClass : MonoBehaviour
{
    public int id;
    public string objectName;
    public Sprite image;
    public float stack;
    public GameObject dropGO;
    bool canInteract, invisible, lightvisible;
    Collider2D m_coll;
    SpriteRenderer spriteRenderer;
    float dropTimer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dropGO.SetActive(false);
        dropTimer = 0;
    }
    void Start() 
    {
        if(spriteRenderer.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            invisible = true;
        }
    }
    void Update()
    {
        if (invisible)
        { //Se invisibile e dentro la camera, mostrare il luccichio ogni 5 secondi
            Vector3 posInView = Camera.main.WorldToViewportPoint(transform.position);
            if((posInView.x >= 0 && posInView.x <= 1) && (posInView.y >= 0 && posInView.y <= 1))
            {
                dropTimer -= Time.deltaTime;
                if(dropTimer <= 0)
                {
                    dropGO.SetActive(true);
                    dropTimer = 5;
                }
            }

            if(dropGO.activeInHierarchy && 
            dropGO.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("drop") && 
            dropGO.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                dropGO.SetActive(false);
            }
        }

        if(canInteract && !invisible)
        {
            if(1 << m_coll.gameObject.layer == 1 << 6)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if(id >= 0 && id <= 6)
                    {
                        addToInventory();
                    }
                    else
                    {
                        addToBag();
                    }
                }
            }
        }
        else if(invisible)                                                  //Se visibile, puoi raccogliere. Se invisibile, deve prima essere stato illuminato
        {
            if(lightvisible)
            {
                if(canInteract)
                {
                    if(1 << m_coll.gameObject.layer == 1 << 6)
                    {
                        if(Input.GetKeyDown(KeyCode.E))
                        {
                            if(id >= 0 && id <= 6)
                            {
                                addToInventory();
                            }
                            else
                            {
                                addToBag();
                            }
                        }
                    }
                }
            }
        }
    }

    void addToInventory()
    {
        switch(id)
        {
            case 1:
                if(!InventoryManager.SMG_obtained)
                {
                    InventoryManager.SMG_obtained = true;
                }
                else
                {
                    InventoryManager.SMG.setAmmo(25);
                }
                break;
            case 2:
                if(!InventoryManager.SG_obtained)
                {
                    InventoryManager.SG_obtained = true;
                }
                else
                {
                    InventoryManager.SG.setAmmo(15);
                }
                break;
            case 3:
                if(!InventoryManager.Torcia_obtained)
                {
                    InventoryManager.Torcia_obtained = true;
                    InventoryManager.addTorcia = true;
                }
                else
                {
                    InventoryManager.addTorcia = true;
                }
                break;
            case 4:
                InventoryManager.addBende++;
                break;
            case 5:
                InventoryManager.addMed = true;
                break;
            case 6:
                InventoryManager.addPollo++;
                break;
        }
        Destroy(this.gameObject);
        SoundManager.PlaySound(SoundManager.Sound.PickUp);
    }

    void addToBag()
    {
        switch(id)
        {
            case 9:
                InventoryManager.SG.setAmmo(15);
                SoundManager.PlaySound(SoundManager.Sound.PickUp);
                Destroy(this.gameObject);
                break;
            case 10:
                InventoryManager.SMG.setAmmo(50);
                SoundManager.PlaySound(SoundManager.Sound.PickUp);
                Destroy(this.gameObject);
                break;
            default:
                InventoryManager inventoryManager = GameObject.Find("GameDirector").GetComponent<InventoryManager>();
                int position = -1;
                bool search = true;
                foreach(InventoryItemClass item in inventoryManager.bag)
                {
                    if(search)
                    {
                        position++;
                        if(item == null)
                        {
                            search = false;
                        }
                    }
                }
                if(position < inventoryManager.bag.Count && !search)
                {
                    inventoryManager.bag[position] = this;
                    gameObject.SetActive(false);
                    SoundManager.PlaySound(SoundManager.Sound.PickUp);
                    InventoryCanvasManager bagManager = GameObject.Find("GameDirector").GetComponent<InventoryCanvasManager>();
                    bagManager.updateBag(position);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            canInteract = true;
            m_coll = other;
        }

        if(1 << other.gameObject.layer == 1 << 15)
        {
            lightvisible = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(1 << other.gameObject.layer == 1 << 6)
        {
            canInteract = false;
            m_coll = null;
        }
    }
}
