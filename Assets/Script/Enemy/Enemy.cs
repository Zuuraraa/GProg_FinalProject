using UnityEngine;

public class Enemy : Character
{
    [SerializeField] SpriteRenderer graphics;
    public Aggression aggro;


    private void Start()
    {
        EnemyStatistics enemyStats = (EnemyStatistics) stats;
        if (enemyStats == null) return;
        if (enemyStats.sprite)
        {
            graphics.sprite = enemyStats.sprite;
        }
        aggro.hasAggro = false;
    }

    public override void OnDeath()
    {
        GameManager.SpawnXPOrb(transform.position, ((EnemyStatistics) stats).xpDrop);
        gameObject.SetActive(false);
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        aggro.hasAggro = true;
    }
}
