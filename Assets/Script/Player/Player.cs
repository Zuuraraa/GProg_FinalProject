using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public static Player instance;
    int xp;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    internal void GainXp(int value)
    {
        xp += value;
    }
}
