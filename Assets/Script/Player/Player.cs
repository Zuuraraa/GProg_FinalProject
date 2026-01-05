using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField] PlayerAction action;
    public static Player instance;

    [Header("Stats")]
    public int maxHP;
    public float speedMult = 1f;

    [Header("Progression")]
    public XPBar xpBar;
    public int xp;
    public int level = 0;
    int maxLevel;

    [Header("Audio Settings")]
    [SerializeField] AudioSource audioSource; 
    [SerializeField] AudioClip hitSound;      
    [SerializeField] AudioClip deathSound;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        maxLevel = ((PlayerStatistics)stats).xpTresholds.Length;
    }

    public override void Reset()
    {
        maxHP = stats.maxHP;
        currentHP = stats.maxHP;
        healthBar.UpdateValue(currentHP, maxHP);
        xpBar.UpdateValue(xp, ((PlayerStatistics)stats).xpTresholds[level]);

        if (animator != null)
        {
            animator.SetBool("IsDead", false);
            animator.Play("Idle"); 
        }
    }


    public override void OnDeath()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (animator != null)
        {
            animator.SetBool("IsDead", true); 
        }

        if (GameOverManager.instance != null)
        {
            GameOverManager.instance.Show();
        }

        if (action != null)
        {
            action.enabled = false; 
        }
        // throw new System.NotImplementedException();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; 
            rb.simulated = false; 
        }

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    internal void GainXp(int value)
    {
        xp += value;
        if (level < maxLevel)
        {
            xpBar.UpdateValue(xp, ((PlayerStatistics)stats).xpTresholds[level]);
            CheckLevelUp();
        }
        else
        {
            xpBar.ReachedMaxXP();
        }
    }

    public void CheckLevelUp()
    {
        if (xp >= ((PlayerStatistics)stats).xpTresholds[level])
        {
            LevelUpPanel.LeveledUp();
        }
    }


    public Item GetItem(int index)
    {
        return action.items[index];
    } 

    protected override void OnDamage(string originCode = "")
    {
        base.OnDamage(originCode);

        healthBar.UpdateValue(currentHP, maxHP);

        if (audioSource != null && hitSound != null)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(hitSound);
        }

        if (animator != null)
        {
            animator.SetTrigger("Hurt"); 
        }
    }
}
