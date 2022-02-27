using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasManager : MonoBehaviour
{
    public List<GameObject> sprites;
    public List<GameObject> selected;
    public List<GameObject> reloads;
    public List<GameObject> spots;
    public List<Text> texts;
    public Text hpText, radText;
    public InventoryManager inventoryManager;
    public PlayerManager playerManager;
    public RectTransform bag;
    void Start()
    {
        hpText.text = playerManager.hpLevelValues[playerManager.levelHp] + "/" + playerManager.hpLevelValues[playerManager.levelHp];
        radText.text = playerManager.radLevelValues[playerManager.levelRad] + "/" + playerManager.radLevelValues[playerManager.levelRad];
        updateSlots();
    }
    void Update()
    {
        if(InventoryManager.SMG_obtained)
        {
            if(!sprites[0].activeSelf)
            {
                sprites[0].SetActive(true);
                reloads[1].SetActive(true);
            }
            texts[2].text = InventoryManager.SMG.getAmmo().ToString();
        }
        if(InventoryManager.SG_obtained)
        {
            if(!sprites[1].activeSelf)
            {
                sprites[1].SetActive(true);
                reloads[2].SetActive(true);
            }
            texts[3].text = InventoryManager.SG.getAmmo().ToString();
        }
        if(InventoryManager.Torcia_obtained)
        {
            if(!sprites[2].activeSelf)
            {
                sprites[2].SetActive(true);
            }
            texts[4].text = inventoryManager.inventory[3].stack.ToString("F1") + "s";
        }


        float count = 0;
        foreach(InventoryItemClass item in inventoryManager.bag)
        {
            if(item.id == 12)
            {
                count += item.stack;
            }
        }
        texts[0].text = "" + count;
        texts[5].text = inventoryManager.inventory[4].stack.ToString();
        texts[6].text = inventoryManager.inventory[5].stack.ToString();
        texts[7].text = inventoryManager.inventory[6].stack.ToString();

        if(!selected[inventoryManager.equipWeapon].activeSelf)
        {
            foreach(GameObject GO in selected)
            {
                if(GO.activeSelf)
                {
                    GO.SetActive(false);
                }
            }
            selected[inventoryManager.equipWeapon].SetActive(true);
        }

        if(playerManager.hp >= 0)
        {
            hpText.text = playerManager.hp + "/" + playerManager.hpLevelValues[playerManager.levelHp];
        }
        else
        {
            hpText.text = "" + 0;
        }
        
        if(playerManager.rad >= 0)
        {
            radText.text = playerManager.rad + "/" + playerManager.radLevelValues[playerManager.levelRad];
        }
        else
        {
            radText.text = "" + 0;
        }
        
        if(Input.GetKey(KeyCode.Tab))
        {
            StopCoroutine("closeBag");
            if(bag.position.x < 0)
            {
                bag.position = Vector3.Lerp(bag.position, new Vector3(0,540,0), 0.1f);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            StartCoroutine("closeBag");
        }
    }

    public void updateSlots()
    {
        for(int i = 0; i < playerManager.bagLevelValues[playerManager.levelBag]; i++)
        {
            spots[i].SetActive(true);
            if(spots[i].transform.childCount > 0)
            {
                removeBag(i);
            }
        }
    }
    public void updateBag(int position)
    {
        GameObject image = new GameObject();
        Image imageComponent = image.AddComponent<Image>();
        RectTransform imageTransform = image.GetComponent<RectTransform>();
        imageTransform.sizeDelta = new Vector2(64,64);
        imageComponent.sprite = inventoryManager.bag[position].image;
        image.transform.SetParent(spots[position].transform);
        imageTransform.anchoredPosition = Vector3.zero;
    }
    public void removeBag(int position)
    {
        Destroy(spots[position].transform.GetChild(0).gameObject);
    }
    public void dropItem(int position)
    {
        if(inventoryManager.bag[position] != null)
        {
            removeBag(position);
            inventoryManager.bag[position].gameObject.transform.position = playerManager.transform.position;
            inventoryManager.bag[position].gameObject.SetActive(true);
            inventoryManager.bag[position] = null;
            inventoryManager.bag[position] = new InventoryItemClass();
        }
    }
    public void startReload(int position, float time)
    {
        Animator animator = reloads[position].GetComponent<Animator>();
        animator.speed = time;
        animator.Play("reload");
    }
    IEnumerator closeBag()
    {
        float timer = 0;
        while(timer <= 1f)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, 1f);
            bag.position = Vector3.Lerp(bag.position, new Vector3(-910, 540,0), timer);
            yield return null;
        }
    }
}
