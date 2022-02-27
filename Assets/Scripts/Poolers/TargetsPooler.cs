using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsPooler : MonoBehaviour
{
    public static TargetsPooler targetPoolInstance;
    public GameObject parent;
    private bool spawnTarget = true;
    private List<GameObject> targetList;

    void Awake() {
        targetPoolInstance = this;
        targetList = new List<GameObject>();
    }

    public GameObject SpawnTarget()
    {
        if(targetList.Count > 0)
        {
            for(int i = 0; i < targetList.Count; i++)
            {
                if(!targetList[i].activeInHierarchy)
                {
                    return targetList[i];
                }
            }
        }

        if(spawnTarget)
        {
            GameObject obst = new GameObject();
            obst.SetActive(false);
            obst.transform.parent = parent.transform;
            targetList.Add(obst);
            return obst;
        }

        return null;
    }
}

