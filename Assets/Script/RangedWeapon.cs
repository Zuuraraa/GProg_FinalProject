using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class RangedWeapon : Weapon
{

    public int poolSize = 120;
    [Header("References")]
    [SerializeField] GameObject projectilePrefab;


    static GameObject[] projectilePool;

    protected override void Awake()
    {
        //base.Awake();
        projectilePool = new GameObject[poolSize];
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab);
            projectileObject.SetActive(false);
            projectilePool[i] = projectileObject;
        }
    }

    protected override IEnumerator AttackProcess()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 destination = ((mousePos - transform.position).normalized + transform.position) * attackSpeed;
        GameObject projectileObj = SpawnAmmo(transform.position);

        if (projectileObj != null)
        {
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            StartCoroutine(projectile.Travel(destination));
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
