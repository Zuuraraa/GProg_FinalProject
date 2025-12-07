using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rb;
    public Animator animator;
    
    int xp;

    public override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    internal void GainXp(int value)
    {
        xp += value;
    }
}
