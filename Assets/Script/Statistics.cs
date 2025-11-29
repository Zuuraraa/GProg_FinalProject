using UnityEngine;

public abstract class Statistics : ScriptableObject
{
    [Header("Hit Points")]
    public int hpMax = 100;
    public int hpCurrent = 100;

    [Header("Base Speed")]
    public float baseMoveSpeed = 10f;
}
