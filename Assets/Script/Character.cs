using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Statistics stats;
    
    public int currentHP;

    Rigidbody2D rb;
    Animator animator;

    [Header("References")]
    [SerializeField] FloatingHealthBar healthbar;

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

    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, stats.maxHP);
        if (currentHP == 0)
        {
            OnDeath();
        }
        else
        {
            OnDamage();
        }
    }
     public Rigidbody2D GetRB() { return rb;}
     public Animator GetAnimator() { return animator; }

    public abstract void OnDeath();
    protected virtual void OnDamage()
    {
        healthbar.UpdateValue(currentHP, stats.maxHP);
    }
}
