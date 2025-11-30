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

    
    protected int damage;
    protected int startSpeed;
    protected int attackSpeed;
    protected int endSpeed;

    private void Awake()
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
        yield return new WaitForSecondsRealtime(FramesToSeconds(startSpeed));
        yield return StartCoroutine(AttackProcess());
        yield return new WaitForSecondsRealtime(FramesToSeconds(endSpeed));
        callback();
    }

    protected abstract IEnumerator AttackProcess();


    protected float FramesToSeconds(int frames)
    {
        return frames * Time.fixedDeltaTime;
    }

    public int GetDamage()
    {
        return damage;
    }
}
