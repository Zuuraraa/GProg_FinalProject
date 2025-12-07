using UnityEngine;


public class Enemy : Character
{
    public Rigidbody2D rb;
    public Transform target;


    private void Start()
    {
        EnemyHandler.enemyList.Add(this);
    }

    public override void OnDeath()
    {
        GameManager.SpawnXPOrb(transform.position + (Vector3) Random.insideUnitCircle * 0.1f, 1);
        gameObject.SetActive(false);
    }
}
