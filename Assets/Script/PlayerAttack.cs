using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player player;
    [SerializeField] GameObject anchor;
    [SerializeField] SpriteRenderer[] sprites;


    [Header("Weapons")]
    [SerializeField] Weapon[] weapons;

    bool isAttacking;
    Vector3 mousePos;
    Weapon currentWeapon;
    
    private void Awake()
    {
        Debug.Assert(weapons.Length != 0, "Weapons cant be empty");
        int size = weapons.Length;
        for (int i = 0; i < size; i++)
        {
            weapons[i] = Instantiate(weapons[i]);
        }
        currentWeapon = weapons[2];
    }

    void Update()
    {
        if (!isAttacking)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FaceMouse();
            if (CheckAttack())
            {
                isAttacking = true;
                StartCoroutine(currentWeapon.PerformAttack(() => { isAttacking = false; }));
            }
        }

    }

    void FaceMouse()
    {
        Vector3 rotation = mousePos - anchor.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        anchor.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        player.animator.SetInteger("Facing", rotation.x >= 0 ? 1 : -1);

        if (rotation.x != 0)
        {
            FlipSprite(rotation.x < 0);
        }
    }


    void FlipSprite(bool value)
    {
        foreach (SpriteRenderer s in sprites)
        {
            s.flipX = value;
        }
    }


    public bool CheckAttack()
    {
        if (currentWeapon.canHoldFire)
        {
            return Input.GetMouseButton(0);
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}