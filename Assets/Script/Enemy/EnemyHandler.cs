using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public static List<Enemy> enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    foreach (Enemy enemy in enemyList)
    //    {
    //        Vector2 targetDistance = (Vector2)enemy.target.position - enemy.rb.position;
    //        if (targetDistance.magnitude > 1f)
    //        {
    //            enemy.rb.position += enemy.stats.baseMoveSpeed * Time.deltaTime * targetDistance.normalized;
    //        }
    //    }
    //}
}
