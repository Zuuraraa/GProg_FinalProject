using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Reference")]
    [SerializeField] Collider2D hurtbox;
    [SerializeField] SpriteRenderer sprite;

    protected override IEnumerator UseProcess()
    {
        hurtbox.enabled = true;
        sprite.enabled = true;
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(attackSpeed));
        hurtbox.enabled = false;
        sprite.enabled = false;
        yield break;
    }

    public override void HandleUse()
    {
        PlayerAction.freezeAiming = true;
        base.HandleUse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(GetDamage());
        }
    }
}
