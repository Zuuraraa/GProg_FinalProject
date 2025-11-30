using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public bool canHoldFire;

    [Header("Upgrades")]
    public float[] damageMultByLevel = { 10f, 15f, 20f };
    public int[] startSpeedByLevel = { 4, 2 , 1 };
    public int[] endSpeedByLevel = { 4, 2, 1 };

    [NonSerialized] public bool unlocked = false;
    [NonSerialized] public int damageLevel = 1;
    [NonSerialized] public int speedLevel = 1;

    public IEnumerator PerformAttack(Action callback)
    {
        yield return new WaitForSeconds(FramesToSeconds(startSpeedByLevel[speedLevel-1]));

        Attack();
        
        yield return new WaitForSeconds(FramesToSeconds(endSpeedByLevel[speedLevel-1]));

        callback();
    }

    protected abstract void Attack();

    float FramesToSeconds(int frames)
    {
        return frames * Time.deltaTime;
    }
}
