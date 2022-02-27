using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class InventoryManager : MonoBehaviour
{
    [Header("Weapon Statistics")]
    public List<float> damageMelee;
    public List<float> damageSMG;
    public List<float> damageSG;
    public List<GameObject> projectiles;
    public List<InventoryItemClass> bag, inventory;
    public int equipWeapon = 0; //0:Melee, 1:SMG, 2:SG
    public static WeaponClass SMG, SG, Melee;
    public static bool SMG_obtained, SG_obtained, Torcia_obtained, addTorcia, addMed;
    public static int addPollo, addBende;
    [Header("References")]
    public InputManager inputManager;
    public PlayerManager playerManager;
    public InventoryCanvasManager inventoryCanvasManager;
    [Header("Value to Add")]
    public int torciaValue;
    public int bendeValue;
    public int medValue;
    public int polloValue;
    void Awake()
    {
        Melee = new WeaponClass(GameData.meleeLevel, damageMelee, 0);
        SMG = new WeaponClass(GameData.smgLevel, damageSMG, GameData.smgAmmo);
        SMG.setProjectile(projectiles[0]);
        SG = new WeaponClass(GameData.sgLevel, damageSG, GameData.sgAmmo);
        SG.setProjectile(projectiles[1]);
        updateBag();
        inventory[3].stack = GameData.torciaValue;
        inventory[4].stack = GameData.bendeValue;
        inventory[5].stack = GameData.medValue;
        inventory[6].stack = GameData.polloValue;
    }

    void Update()
    {
        if(inputManager.changeInventoryUp && !playerManager.cantChange)
        {
            if(equipWeapon < 6)
            {
                equipWeapon++;
            }
        }
        
        if(inputManager.changeInventoryDown && !playerManager.cantChange)
        {
            if(equipWeapon > 0)
            {
                equipWeapon--;
            }
        }

        if(addTorcia)
        {
            inventory[3].stack += torciaValue;
            addTorcia = false;
        }
        if(addBende > 0)
        {
            inventory[4].stack += bendeValue;
            addBende--;
        }
        if(addMed)
        {
            inventory[5].stack += medValue;
            addMed = false;
        }
        if(addPollo > 0)
        {
            inventory[6].stack += polloValue;
            addPollo--;
        }
    }
    public void updateBag()
    {
        bag = new List<InventoryItemClass>();
        for(int i = 0; i < playerManager.bagLevelValues[playerManager.levelBag]; i++)
        {
            bag.Add(new InventoryItemClass());
        }
        inventoryCanvasManager.updateSlots();
    }
}
