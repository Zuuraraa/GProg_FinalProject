using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("XP Orbs")]
    public GameObject xpOrbPrefab;
    public int xpOrbCount = 100;
    
    static List<GameObject> xpOrbs;
    static GameManager instance;

    private void Awake()
    {
        instance = this;
        xpOrbs = new List<GameObject>();
        for (int i = 0; i < xpOrbCount; i++)
        {
            CreateXPOrb();
        }
    }

    public static void SpawnXPOrb(Vector3 position, int value)
    {
        foreach (GameObject obj in xpOrbs)
        {
            if (obj.activeSelf == false)
            {
                SetUpOrb(obj, position, value);  
                return;
            }
        }
        SetUpOrb(CreateXPOrb(), position, value);
        
    }
    
    static GameObject CreateXPOrb()
    {
        GameObject orb = Instantiate(instance.xpOrbPrefab);
        orb.SetActive(false);
        xpOrbs.Add(orb);
        return orb;
    }

    static void SetUpOrb(GameObject xpOrb, Vector3 position, int value)
    {
        XPOrb xpOrbScript = xpOrb.GetComponent<XPOrb>();
        xpOrb.transform.position = position;
        xpOrbScript.value = value;
        xpOrbScript.SetTarget(null);
        xpOrb.SetActive(true);
    }

}
