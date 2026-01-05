using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public bool canHoldFire;
    public Sprite sprite;
    [TextArea] public string description;

    [Header("Stats")]
    public int[] damageByLevel = { 5, 10, 15 };
    public int[] startSpeedByLevel = { 4, 2, 1 };
    public int[] attackSpeedByLevel = { 4, 2, 1 };
    public int[] endSpeedByLevel = { 4, 2, 1 };

    [Header("Visuals")]
    public int animationType;

    public int baseFrameCount = 60;
}
