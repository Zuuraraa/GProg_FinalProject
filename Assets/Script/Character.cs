using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Statistics stats;
    
    public int currentHP;

    protected Rigidbody2D rb;
    protected Animator animator;

    [Header("References")]
    public HealthBar healthBar;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        Reset();
    }

    public virtual void Reset()
    {
        currentHP = stats.maxHP;
    }

    public void TakeDamage(int damage, string originCode = "")
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.maxHP);
        if (currentHP == 0)
        {
            OnDeath();
        }
        else
        {
            OnDamage(originCode);
        }
    }
     public Rigidbody2D GetRB() { return rb;}
     public Animator GetAnimator() { return animator; }

    public abstract void OnDeath();
    protected virtual void OnDamage(string originCode = "")
    {
        healthBar.UpdateValue(currentHP, stats.maxHP);
    }
}
