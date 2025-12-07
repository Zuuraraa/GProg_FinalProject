using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Statistics stats;
    public int currentHP;

    [SerializeField] FloatingHealthBar healthbar;
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.maxHP);
        healthbar.UpdateValue(currentHP, stats.maxHP);
        if (currentHP == 0)
        {
            OnDeath();
        }
    }

    public abstract void OnDeath();
}
