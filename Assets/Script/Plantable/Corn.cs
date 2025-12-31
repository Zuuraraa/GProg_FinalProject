using System.Collections.Generic;
using UnityEngine;

public class Corn : PlantAction
{
    public int poolSize = 4;

    [Header("References")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Projectile Stats")]
    public float projectileSpeed = 10f;
    public int projectileLifespan = 30;

    List<GameObject> projectilePool;
    Vector3[] directions =
    {
        Vector3.up,
        Vector3.right,
        Vector3.down,
        Vector3.left,
    };

    protected void Awake()
    {
        projectilePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab);
            projectileObject.SetActive(false);
            projectilePool.Add(projectileObject);
        }
    }

    public override void DoAction()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectileObj= SpawnProjectile(i, plant.transform.position);
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.SetDamage(plant.plantData.damage);
            projectile.StartCoroutine(projectile.Travel(directions[i], projectileSpeed, GameManager.FramesToSeconds(projectileLifespan)));

        }
    }

    GameObject SpawnProjectile(int idx, Vector3 location)
    {
        GameObject ammo = projectilePool[idx];
        ammo.transform.position = location;
        ammo.SetActive(true);
        return ammo;
    }
}
