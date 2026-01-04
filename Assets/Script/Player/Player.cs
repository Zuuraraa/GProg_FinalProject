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
    [SerializeField] AudioSource audioSource; // Jangan lupa pasang di Inspector!
    [SerializeField] AudioClip hitSound;      // Suara "Aduh!"
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
    }


    public override void OnDeath()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        // throw new System.NotImplementedException();
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
    }
}
