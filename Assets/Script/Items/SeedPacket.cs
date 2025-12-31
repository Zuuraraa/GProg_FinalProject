using System;
using System.Collections;
using UnityEngine;

public class SeedPacket : Item
{
    [SerializeField] int packetCount = 0;


    public override void CheckUse()
    {
        if (Input.GetMouseButtonDown(0) && PlantingManager.canPlant && packetCount > 0) { HandleUse(); }
    }

    public override void HandleUse()
    {
        Debug.Log("Planted here");
        packetCount--;
    }
}
