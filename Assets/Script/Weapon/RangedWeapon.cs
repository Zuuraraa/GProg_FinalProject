using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{

    public int poolSize = 20;
    [Header("References")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform attackPointer;


    [Header("Special Weapon Stats")]
    public float projectileSpeed = 10f;
    public int projectileLifespan = 60;


    static List<GameObject> projectilePool;

    protected override void Awake()
    {
        //base.Awake();
        projectilePool = new List<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab);
            projectileObject.SetActive(false);
            projectilePool.Add(projectileObject);
        }
    }

    protected override IEnumerator AttackProcess()
    {
        GameObject projectileObj = SpawnAmmo(transform.position);
        Vector3 direction = (attackPointer.position - transform.position);
        direction += new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0);

        if (projectileObj != null)
        {
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            StartCoroutine(projectile.Travel(Vector3.Normalize(direction), projectileSpeed, GameManager.FramesToSeconds(projectileLifespan)));
        }
        yield break;
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in projectilePool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    } 

}
