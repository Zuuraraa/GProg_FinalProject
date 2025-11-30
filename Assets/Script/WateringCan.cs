using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WateringCan", menuName = "Scriptable Objects/Weapon/Watering Can")]
public class WateringCan : Weapon
{
    protected override void Attack()
    {
        Debug.Log("Watering Can Attack");
    }
}
