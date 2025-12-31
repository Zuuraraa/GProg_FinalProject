using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    public int maxHP;

    [Header("Progression")]
    [SerializeField] XPBar xpBar;
    public int xp;
    public int level = 0;
    int maxLevel;

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
        throw new System.NotImplementedException();
    }

    internal void GainXp(int value)
    {
        xp += value;
        if (level < maxLevel)
        {
            xpBar.UpdateValue(xp, ((PlayerStatistics)stats).xpTresholds[level]);
        }
        else
        {
            xpBar.ReachedMaxHP();
        }
    }

    protected override void OnDamage(string originCode = "")
    {
        healthBar.UpdateValue(currentHP, maxHP);
    }
}
