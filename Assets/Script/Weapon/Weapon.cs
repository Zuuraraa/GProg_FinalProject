using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData info;
    public bool unlocked;


    [Header("Level")]
    public int damageLevel = 1;
    public int speedLevel = 1;
    
    protected int damage = 10;
    protected int startSpeed = 4;
    protected int attackSpeed = 4;
    protected int endSpeed = 4;

    protected virtual void Awake()
    {
        SetDamageLevel(damageLevel); 
        SetSpeedLevel(speedLevel);
    }

    void SetDamageLevel(int level)
    {
        damageLevel = level;
        damage = info.damageByLevel[level - 1];
    }

    void SetSpeedLevel(int level)
    {
        speedLevel = level;
        startSpeed = info.startSpeedByLevel[speedLevel - 1];
        attackSpeed = info.attackSpeedByLevel[speedLevel - 1];
        endSpeed = info.endSpeedByLevel[speedLevel - 1];
    }

    public IEnumerator PerformAttack(Action callback)
    {
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(startSpeed));
        yield return StartCoroutine(AttackProcess());
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(endSpeed));
        callback();
    }

    protected abstract IEnumerator AttackProcess();

    public int GetDamage()
    {
        return damage;
    }
}
