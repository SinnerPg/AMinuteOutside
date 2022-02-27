using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelterManager : MonoBehaviour
{
    public static float acciaio, acqua, batterie, benza, ciboInScatola, legno, plastica, riso;
    public InventoryManager inventoryManager;
    public PlayerManager playerManager;
    public GameData gameData;
    public Button shelterButton;
    public GameObject shelterCanvas, hud, bagCanvas;
    public List<float> bottleValues, risoValues, ciboValues, benzaValues, legnoValues, acciaioValues, plasticaValues;
    public List<Text> actualValue, levelUpValue, bottleNeeded, risoNeeded, ciboNeeded, benzaNeeded, legnoNeeded, acciaioNeeded, plasticaNeeded;
    ResetUtils utils;
    void Awake()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        utils = GameObject.Find("ResetUtils").GetComponent<ResetUtils>();
    }

    void Start()
    {
        acciaio = GameData.acciaio;
        acqua = GameData.acqua;
        batterie = GameData.batterie;
        benza = GameData.benza;
        ciboInScatola = GameData.ciboInScatola;
        legno = GameData.legno;
        plastica = GameData.plastica;
        riso = GameData.riso;
    }

    public void magazineBag() //Svuotare lo zaino per riempire il rifugio
    {
        int position = -1;
        foreach(InventoryItemClass item in inventoryManager.bag)
        {
            position++;
            if(item != null)
            {
                switch(item.id)
                {
                    case 7:
                        acciaio += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 8:
                        acqua += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 11:
                        batterie += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 12:
                        benza += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 13:
                        ciboInScatola += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 14:
                        legno += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 15:
                        plastica += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                    case 16:
                        riso += item.stack;
                        Destroy(inventoryManager.bag[position].gameObject);
                        break;
                }
            }
        }
    }
    public void buildCanvas()
    {
        //Valori attuali
        actualValue[0].text = "" + playerManager.hpLevelValues[playerManager.levelHp];
        actualValue[1].text = "" + playerManager.radLevelValues[playerManager.levelRad];
        actualValue[2].text = "" + playerManager.speedLevelValues[playerManager.levelSpeed];
        actualValue[3].text = "" + playerManager.jetpackLevel[playerManager.levelJetpack];
        actualValue[4].text = "" + playerManager.bagLevelValues[playerManager.levelBag];
        actualValue[5].text = "" + InventoryManager.Melee.getDamage();
        actualValue[6].text = "" + InventoryManager.SMG.getDamage();
        actualValue[7].text = "" + InventoryManager.SG.getDamage();

        //Valori futuri
        if(playerManager.levelHp < 4)
        {
            levelUpValue[0].text = "" + playerManager.hpLevelValues[playerManager.levelHp + 1];   
        }
        else
        {
            levelUpValue[0].text = "MAX";
        }
        if(playerManager.levelRad < 4)
        {
            levelUpValue[1].text = "" + playerManager.radLevelValues[playerManager.levelRad + 1];   
        }
        else
        {
            levelUpValue[1].text = "MAX";
        }
        if(playerManager.levelSpeed < 4)
        {
            levelUpValue[2].text = "" + playerManager.speedLevelValues[playerManager.levelSpeed + 1];   
        }
        else
        {
            levelUpValue[2].text = "MAX";
        }
        if(playerManager.levelJetpack < 4)
        {
            levelUpValue[3].text = "" + playerManager.jetpackLevel[playerManager.levelJetpack + 1];   
        }
        else
        {
            levelUpValue[3].text = "MAX";
        }
        if(playerManager.levelBag < 4)
        {
            levelUpValue[4].text = "" + playerManager.bagLevelValues[playerManager.levelBag + 1];   
        }
        else
        {
            levelUpValue[4].text = "MAX";
        }
        if(InventoryManager.Melee.getLevel() < 4)
        {
            levelUpValue[5].text = "" + InventoryManager.Melee.getDamageList()[InventoryManager.Melee.getLevel() + 1];   
        }
        else
        {
            levelUpValue[5].text = "MAX";
        }
        if(InventoryManager.SMG.getLevel() < 4)
        {
            levelUpValue[6].text = "" + InventoryManager.SMG.getDamageList()[InventoryManager.SMG.getLevel() + 1];   
        }
        else
        {
            levelUpValue[6].text = "MAX";
        }
        if(InventoryManager.SG.getLevel() < 4)
        {
            levelUpValue[7].text = "" + InventoryManager.SG.getDamageList()[InventoryManager.SG.getLevel() + 1];   
        }
        else
        {
            levelUpValue[7].text = "MAX";
        }

        //Materiali necessari
        if(playerManager.levelHp < 4)
        {
            bottleNeeded[0].text = acqua + "/" + bottleValues[playerManager.levelHp];   
            risoNeeded[0].text = riso + "/" + risoValues[playerManager.levelHp];
            ciboNeeded[0].text = ciboInScatola + "/" + ciboValues[playerManager.levelHp];
        }
        else
        {
            bottleNeeded[0].text = "MAX";   
            risoNeeded[0].text = "MAX";
            ciboNeeded[0].text = "MAX";
        }
        if(playerManager.levelRad < 4)
        {
            bottleNeeded[1].text = acqua + "/" + bottleValues[playerManager.levelRad];   
            risoNeeded[1].text = riso + "/" + risoValues[playerManager.levelRad];
            ciboNeeded[1].text = ciboInScatola + "/" + ciboValues[playerManager.levelRad];
        }
        else
        {
            bottleNeeded[1].text = "MAX";   
            risoNeeded[1].text = "MAX";
            ciboNeeded[1].text = "MAX";
        }
        if(playerManager.levelSpeed < 4)
        {
            bottleNeeded[2].text = acqua + "/" + bottleValues[playerManager.levelSpeed];   
            risoNeeded[2].text = riso + "/" + risoValues[playerManager.levelSpeed];
            ciboNeeded[2].text = ciboInScatola + "/" + ciboValues[playerManager.levelSpeed];
        }
        else
        {
            bottleNeeded[2].text = "MAX";   
            risoNeeded[2].text = "MAX";
            ciboNeeded[2].text = "MAX";
        }
        if(playerManager.levelJetpack < 4)
        {
            benzaNeeded[0].text = benza + "/" + benzaValues[playerManager.levelJetpack];   
            legnoNeeded[0].text = legno + "/" + legnoValues[playerManager.levelJetpack];
            acciaioNeeded[0].text = acciaio + "/" + acciaioValues[playerManager.levelJetpack];
            plasticaNeeded[0].text = plastica + "/" + plasticaValues[playerManager.levelJetpack];
        }
        else
        {
            benzaNeeded[0].text = "MAX";   
            legnoNeeded[0].text = "MAX";
            acciaioNeeded[0].text = "MAX";
            plasticaNeeded[0].text = "MAX";
        }
        if(playerManager.levelBag < 4)
        {
            benzaNeeded[1].text = benza + "/" + benzaValues[playerManager.levelBag];   
            legnoNeeded[1].text = legno + "/" + legnoValues[playerManager.levelBag];
            acciaioNeeded[1].text = acciaio + "/" + acciaioValues[playerManager.levelBag];
            plasticaNeeded[1].text = plastica + "/" + plasticaValues[playerManager.levelBag];
        }
        else
        {
            benzaNeeded[1].text = "MAX";   
            legnoNeeded[1].text = "MAX";
            acciaioNeeded[1].text = "MAX";
            plasticaNeeded[1].text = "MAX";
        }
        if(InventoryManager.Melee.getLevel() < 4)
        {
            int level = InventoryManager.Melee.getLevel();
            benzaNeeded[2].text = benza + "/" + benzaValues[level];   
            legnoNeeded[2].text = legno + "/" + legnoValues[level];
            acciaioNeeded[2].text = acciaio + "/" + acciaioValues[level];
            plasticaNeeded[2].text = plastica + "/" + plasticaValues[level];
        }
        else
        {
            benzaNeeded[2].text = "MAX";   
            legnoNeeded[2].text = "MAX";
            acciaioNeeded[2].text = "MAX";
            plasticaNeeded[2].text = "MAX";
        }
        if(InventoryManager.SMG.getLevel() < 4)
        {
            int level = InventoryManager.SMG.getLevel();
            benzaNeeded[3].text = benza + "/" + benzaValues[level];   
            legnoNeeded[3].text = legno + "/" + legnoValues[level];
            acciaioNeeded[3].text = acciaio + "/" + acciaioValues[level];
            plasticaNeeded[3].text = plastica + "/" + plasticaValues[level];
        }
        else
        {
            benzaNeeded[3].text = "MAX";   
            legnoNeeded[3].text = "MAX";
            acciaioNeeded[3].text = "MAX";
            plasticaNeeded[3].text = "MAX";
        }
        if(InventoryManager.SG.getLevel() < 4)
        {
            int level = InventoryManager.SG.getLevel();
            benzaNeeded[4].text = benza + "/" + benzaValues[level];   
            legnoNeeded[4].text = legno + "/" + legnoValues[level];
            acciaioNeeded[4].text = acciaio + "/" + acciaioValues[level];
            plasticaNeeded[4].text = plastica + "/" + plasticaValues[level];
        }
        else
        {
            benzaNeeded[4].text = "MAX";   
            legnoNeeded[4].text = "MAX";
            acciaioNeeded[4].text = "MAX";
            plasticaNeeded[4].text = "MAX";
        }
    }
    public void levelUp(int id)
    {
        switch(id)
        {
            case 0:
                if(playerManager.levelHp < 4)
                {
                    if(acqua >= bottleValues[playerManager.levelHp] && 
                    riso >= risoValues[playerManager.levelHp] &&
                    ciboInScatola >= ciboValues[playerManager.levelHp])
                    {
                        acqua -= bottleValues[playerManager.levelHp];
                        riso -= risoValues[playerManager.levelHp];
                        ciboInScatola -= ciboValues[playerManager.levelHp];
                        playerManager.levelHp++;
                        buildCanvas();
                    }
                }
                break;
            case 1:
                if(playerManager.levelRad < 4)
                {
                    if(acqua >= bottleValues[playerManager.levelRad] && 
                    riso >= risoValues[playerManager.levelRad] &&
                    ciboInScatola >= ciboValues[playerManager.levelRad])
                    {
                        acqua -= bottleValues[playerManager.levelRad];
                        riso -= risoValues[playerManager.levelRad];
                        ciboInScatola -= ciboValues[playerManager.levelRad];
                        playerManager.levelRad++;
                        buildCanvas();
                    }
                }
                break;
            case 2:
                if(playerManager.levelSpeed < 4)
                {
                    if(acqua >= bottleValues[playerManager.levelSpeed] && 
                    riso >= risoValues[playerManager.levelSpeed] &&
                    ciboInScatola >= ciboValues[playerManager.levelSpeed])
                    {
                        acqua -= bottleValues[playerManager.levelSpeed];
                        riso -= risoValues[playerManager.levelSpeed];
                        ciboInScatola -= ciboValues[playerManager.levelSpeed];
                        playerManager.levelSpeed++;
                        buildCanvas();
                    }
                }
                break;
            case 3:
                if(playerManager.levelJetpack < 4)
                {
                    if(benza >= benzaValues[playerManager.levelJetpack] && 
                    legno >= legnoValues[playerManager.levelJetpack] &&
                    acciaio >= acciaioValues[playerManager.levelJetpack] &&
                    plastica >= plasticaValues[playerManager.levelJetpack])
                    {
                        benza -= benzaValues[playerManager.levelJetpack];
                        legno -= legnoValues[playerManager.levelJetpack];
                        acciaio -= acciaioValues[playerManager.levelJetpack];
                        plastica -= plasticaValues[playerManager.levelJetpack];
                        playerManager.levelJetpack++;
                        buildCanvas();
                    }
                }
                break;
            case 4:
                if(playerManager.levelBag < 4)
                {
                    if(benza >= benzaValues[playerManager.levelBag] && 
                    legno >= legnoValues[playerManager.levelBag] &&
                    acciaio >= acciaioValues[playerManager.levelBag] &&
                    plastica >= plasticaValues[playerManager.levelBag])
                    {
                        benza -= benzaValues[playerManager.levelBag];
                        legno -= legnoValues[playerManager.levelBag];
                        acciaio -= acciaioValues[playerManager.levelBag];
                        plastica -= plasticaValues[playerManager.levelBag];
                        playerManager.levelBag++;
                        buildCanvas();
                    }
                }
                break;
            case 5:
                int level = InventoryManager.Melee.getLevel();
                if(level < 4)
                {
                    if(benza >= benzaValues[level] && 
                    legno >= legnoValues[level] &&
                    acciaio >= acciaioValues[level] &&
                    plastica >= plasticaValues[level])
                    {
                        benza -= benzaValues[level];
                        legno -= legnoValues[level];
                        acciaio -= acciaioValues[level];
                        plastica -= plasticaValues[level];
                        InventoryManager.Melee.setLevel(level + 1);
                        buildCanvas();
                    }
                }
                break;
            case 6:
                int level2 = InventoryManager.SMG.getLevel();
                if(level2 < 4)
                {
                    if(benza >= benzaValues[level2] && 
                    legno >= legnoValues[level2] &&
                    acciaio >= acciaioValues[level2] &&
                    plastica >= plasticaValues[level2])
                    {
                        benza -= benzaValues[level2];
                        legno -= legnoValues[level2];
                        acciaio -= acciaioValues[level2];
                        plastica -= plasticaValues[level2];
                        InventoryManager.SMG.setLevel(level2 + 1);
                        buildCanvas();
                    }
                }
                break;
            case 7:
                int level3 = InventoryManager.SG.getLevel();
                if(level3 < 4)
                {
                    if(benza >= benzaValues[level3] && 
                    legno >= legnoValues[level3] &&
                    acciaio >= acciaioValues[level3] &&
                    plastica >= plasticaValues[level3])
                    {
                        benza -= benzaValues[level3];
                        legno -= legnoValues[level3];
                        acciaio -= acciaioValues[level3];
                        plastica -= plasticaValues[level3];
                        InventoryManager.SG.setLevel(level3 + 1);
                        buildCanvas();
                    }
                }
                break;
        }
    }
    public void newSpedition()
    {
        shelterButton.enabled = false;
        if(utils == null) GameObject.Find("ResetUtils").GetComponent<ResetUtils>();
        GameManager.day += 1;

        //Player
        GameData.hpLevelPlayer = playerManager.levelHp;
        GameData.radLevelPlayer = playerManager.levelRad;
        GameData.bagLevelPlayer = playerManager.levelBag;
        GameData.speedLevelPlayer = playerManager.levelSpeed;
        GameData.jetpackLevelPlayer = playerManager.levelJetpack;
        //GameManager
        GameData.nKill = GameManager.nKill;
        GameData.day = GameManager.day;
        //Shelter
        GameData.acciaio = acciaio;
        GameData.acqua = acqua;
        GameData.batterie = batterie;
        GameData.benza = benza;
        GameData.ciboInScatola = ciboInScatola;
        GameData.legno = legno;
        GameData.plastica = plastica;
        GameData.riso = riso;
        //Inventory
        GameData.smgLevel = InventoryManager.Melee.getLevel();
        GameData.sgLevel = InventoryManager.Melee.getLevel();
        GameData.smgAmmo = InventoryManager.SMG.getAmmo();
        GameData.sgAmmo = InventoryManager.SG.getAmmo();
        GameData.torciaValue = inventoryManager.inventory[3].stack;
        GameData.bendeValue = inventoryManager.inventory[4].stack;
        GameData.medValue = inventoryManager.inventory[5].stack;
        GameData.polloValue = inventoryManager.inventory[6].stack;
        //Tutorial
        GameData.hasPlayed = true;

        gameData.save();

        inventoryManager.updateBag();
        playerManager.updateStats();
        Time.timeScale = 1;
        if(GameManager.day < 5) utils.resetGame();
        else utils.lastScene();
    }
}
