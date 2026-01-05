using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    public SpriteRenderer graphics;
    public EnemyAggro aggro;

    [Header("Audio Settings")]
    public AudioSource audioSource; 
    public AudioClip hitSound;      
    public AudioClip deathSound;


    public override void Reset()
    {
        EnemyStatistics enemyStats = (EnemyStatistics) stats;
        if (enemyStats == null) { return; }
        base.Reset();
        if (enemyStats.isBoss)
        {
            transform.localScale = Vector3.one * 4;
            ((FloatingHealthBar)healthBar).offset.y = 2.4f;
        }
        else
        {
            transform.localScale = Vector3.one;
            ((FloatingHealthBar)healthBar).offset.y = 0.6f;
        }

        if (enemyStats.sprite)
        {
            graphics.sprite = enemyStats.sprite;
        }
        aggro.hasAggro = false;
    }

    public override void OnDeath()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        GameManager.SpawnXPOrb(transform.position, ((EnemyStatistics) stats).xpDrop);
        GameManager.SpawnRandomDrop(transform.position);
        gameObject.SetActive(false);
    }

    protected override void OnDamage(string originCode = "")
    {
        base.OnDamage(originCode);
        
        if (audioSource != null && hitSound != null)
        {
            audioSource.pitch = Random.Range(0.85f, 1.15f); // Variasi nada dikit
            audioSource.PlayOneShot(hitSound);
        }

        if (originCode == "Player")
        {
            aggro.hasAggro = true;
        }
    }
}
