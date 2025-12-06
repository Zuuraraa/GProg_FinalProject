using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Statistics stats;
    public int currentHP;
    


    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.maxHP);

    }
}
