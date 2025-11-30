using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Sickle", menuName = "Scriptable Objects/Weapon/Sickle")]
public class Sickle : Weapon
{
    protected override void Attack()
    {
        Debug.Log("Sickle Attack Start");
    }

}
