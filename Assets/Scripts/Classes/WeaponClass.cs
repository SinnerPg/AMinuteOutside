using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass
{
    int level, ammo;
    float damage;
    GameObject projectile;
    public List<float> damageList;
    public WeaponClass(int level, List<float> damageList, int ammo)
    {
        this.level = level;
        this.damageList = damageList;
        this.ammo = ammo;
        this.damage = damageList[getLevel()];
    }
    public void setDamage()
    {
        this.damage = damageList[getLevel()];
    }
    public float getDamage()
    {
        return damage;
    }
    public List<float> getDamageList()
    {
        return damageList;
    }
    public void setLevel(int level)
    {
        this.level = level;
        setDamage();
    }
    public int getLevel()
    {
        return level;
    }
    public void setProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }
    public GameObject getProjectile()
    {
        return projectile;
    }
    public int getAmmo()
    {
        return ammo;
    }
    public void setAmmo(int ammo)
    {
        this.ammo += ammo;
    }
}
