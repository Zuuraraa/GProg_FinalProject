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
