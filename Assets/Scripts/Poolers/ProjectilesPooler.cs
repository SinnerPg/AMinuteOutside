using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPooler : MonoBehaviour
{
    public static ProjectilesPooler projectilePoolInstance;
    public GameObject parent;
    private bool spawnSMG = true, spawnSG = true;
    private List<GameObject> smgList, sgList;

    void Awake() {
        projectilePoolInstance = this;
        smgList = new List<GameObject>();
        sgList = new List<GameObject>();
    }
    public GameObject SpawnSMGProjectile()
    {
        if(smgList.Count > 0)
        {
            for(int i = 0; i < smgList.Count; i++)
            {
                if(!smgList[i].activeInHierarchy)
                {
                    smgList[i].GetComponent<ProjectileManager>().enabled = true;
                    return smgList[i];
                }
            }
        }

        if(spawnSMG)
        {
            GameObject obst = Instantiate(InventoryManager.SMG.getProjectile());
            obst.SetActive(false);
            smgList.Add(obst);
            return obst;
        }

        return null;
    }
    public GameObject SpawnSGProjectile()
    {
        if(sgList.Count > 0)
        {
            for(int i = 0; i < sgList.Count; i++)
            {
                if(!sgList[i].activeInHierarchy)
                {
                    sgList[i].GetComponent<ProjectileManager>().enabled = true;
                    return sgList[i];
                }
            }
        }

        if(spawnSG)
        {
            GameObject obst = Instantiate(InventoryManager.SG.getProjectile());
            obst.SetActive(false);
            obst.transform.parent = parent.transform;
            sgList.Add(obst);
            return obst;
        }

        return null;
    }
}

