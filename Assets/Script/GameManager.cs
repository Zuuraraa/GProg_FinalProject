using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [Header("Pickups")]
    public GameObject hpPickupPrefab;
    public GameObject seedPickupPrefab;
    public GameObject xpOrbPrefab;
    public int pickUpCount = 25;

    static List<GameObject> xpOrbs;
    static List<GameObject> hpPickups;
    static List<GameObject> seedPickups;
    static GameManager instance;

    private void Awake()
    {
        instance = this;
        xpOrbs = new List<GameObject>();
        hpPickups= new List<GameObject>();
        seedPickups = new List<GameObject>();
        for (int i = 0; i < pickUpCount; i++)
        {
            CreatePickup(xpOrbs, xpOrbPrefab);
            CreatePickup(hpPickups, hpPickupPrefab);
            CreatePickup(seedPickups, seedPickupPrefab);

        }
    }


    public static float FramesToSeconds(int frames)
    {
        return frames * Time.fixedDeltaTime;
    }

    public static void SpawnXPOrb(Vector3 position, int value)
    {
        SpawnPickable(xpOrbs, instance.xpOrbPrefab, "XPOrb", position, value);
        
    }
    
    public static void SpawnRandomDrop(Vector3 position)
    {
        switch (Random.Range(0,10))
        {
            case 0:
                SpawnPickable(hpPickups, instance.hpPickupPrefab, "HealthPickup", position);
                break;
            case 1:
                SpawnPickable(seedPickups, instance.seedPickupPrefab, "SeedPickup", position, Random.Range(0,2));
                break;
            default:
                break;
        }
    }

    public static void SpawnPickable(List<GameObject> list, GameObject prefab, string type, Vector3 position, int value = -1)
    {
        foreach (GameObject obj in list)
        {
            if (obj.activeSelf == false)
            {
                SetupPickable(obj, type, position, value);
                return;
            }
        }
        SetupPickable(CreatePickup(list, prefab), type, position, value);

    }

    static GameObject CreatePickup(List<GameObject> list, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        list.Add(obj);
        return obj;
    }

    static void SetupPickable(GameObject obj, string type, Vector3 position, int value)
    {
        Pickable script = (Pickable) (obj.GetComponent(type));
        obj.transform.position = position;
        script.Reset();
        switch (type) {
            case "XPOrb":
                ((XPOrb)script).value = value;
                break;
            case "SeedPickup":
                ((SeedPickup)script).seedType = (SeedPickup.SeedType)(value);
                break;
        }
        obj.SetActive(true);
    }
}
