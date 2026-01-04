using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    public SpriteRenderer graphics;
    public EnemyAggro aggro;


    public override void Reset()
    {
        EnemyStatistics enemyStats = (EnemyStatistics) stats;
        if (enemyStats == null) { return; }
        base.Reset();
        if (enemyStats.sprite)
        {
            graphics.sprite = enemyStats.sprite;
        }
        aggro.hasAggro = false;
    }

    public override void OnDeath()
    {
        GameManager.SpawnXPOrb(transform.position, ((EnemyStatistics) stats).xpDrop);
        GameManager.SpawnRandomDrop(transform.position);
        gameObject.SetActive(false);
    }

    protected override void OnDamage(string originCode = "")
    {
        base.OnDamage(originCode);
        if (originCode == "Player")
        {
            aggro.hasAggro = true;
        }
    }
}
