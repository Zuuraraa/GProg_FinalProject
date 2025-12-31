using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : Item
{
    public WeaponData info;
    public bool unlocked;


    [Header("Level")]
    public int level = 1;
    
    protected int damage = 10;
    protected int startSpeed = 4;
    protected int attackSpeed = 4;
    protected int endSpeed = 4;

    protected virtual void Awake()
    {
        SetLevel(level); 
    }

    public override bool ValidToSwapInto()
    {
        return unlocked;
    }

    public override bool ValidToSwapOut()
    {
        return !PlayerAction.isAttacking;
    }

    public override void CheckUse()
    {
        bool validMouse = (info.canHoldFire ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0));
        if (validMouse && !PlayerAction.isAttacking) { HandleUse(); }
    }

    public override void HandleUse()
    {
        PlayerAction.isAttacking = true;
        StartCoroutine(Use(() => {
            PlayerAction.freezeAiming = false;
            PlayerAction.isAttacking = false;
        }));
    }

    public IEnumerator Use(Action callback)
    {
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(startSpeed));
        yield return StartCoroutine(UseProcess());
        yield return new WaitForSecondsRealtime(GameManager.FramesToSeconds(endSpeed));
        callback();
    }

    protected abstract IEnumerator UseProcess();

    void SetLevel(int newLevel)
    {
        level = newLevel;
        damage = info.damageByLevel[level - 1];
        startSpeed = info.startSpeedByLevel[level - 1];
        attackSpeed = info.attackSpeedByLevel[level - 1];
        endSpeed = info.endSpeedByLevel[level - 1];
    }

    public int GetDamage()
    {
        return damage;
    }
}
