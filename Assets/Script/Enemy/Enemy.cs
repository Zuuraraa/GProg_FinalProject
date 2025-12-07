using UnityEngine;


public class Enemy : Character
{
    public Rigidbody2D rb;
    public Transform target;


    private void Start()
    {
        EnemyHandler.enemyList.Add(this);
    }
}
