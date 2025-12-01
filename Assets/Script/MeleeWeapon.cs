using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Reference")]
    [SerializeField] Collider2D hurtbox;
    [SerializeField] SpriteRenderer sprite;

    protected override IEnumerator AttackProcess()
    {
        hurtbox.enabled = true;
        sprite.enabled = true;
        yield return new WaitForSecondsRealtime(FramesToSeconds(attackSpeed));
        hurtbox.enabled = false;
        sprite.enabled = false;
        yield break;
    }

}
