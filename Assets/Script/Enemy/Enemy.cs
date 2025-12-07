using UnityEngine;

public class Enemy : Character
{
    [SerializeField] SpriteRenderer graphics;
    [SerializeField] Aggression aggro;

    private void Start()
    {
        EnemyStatistics enemyStats = (EnemyStatistics) stats;
        if (enemyStats.sprite)
        {
            graphics.sprite = enemyStats.sprite;
        }
        //EnemyHandler.enemyList.Add(this);
    }

    public override void OnDeath()
    {
        GameManager.SpawnXPOrb(transform.position, ((EnemyStatistics) stats).xpDrop);
        gameObject.SetActive(false);
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        aggro.enabled = true;
        aggro.GainAggro();
    }
}
