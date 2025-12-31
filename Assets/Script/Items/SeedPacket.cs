using System;
using System.Collections;
using UnityEngine;

public class SeedPacket : Item
{
    public GameObject plantPrefab;
    [SerializeField] int packetCount = 0;


    public override void CheckUse()
    {
        if (Input.GetMouseButtonDown(0) && PlantingManager.IsCurrentPositionClear() && PlantingManager.canPlant && packetCount > 0) 
        { 
            HandleUse(); 
        }
    }

    public override void HandleSwapIn()
    {
        Plant plant = plantPrefab.GetComponent<Plant>();
        PlantingManager.SetPreviewImage(plant.plantData.sprite);
    }

    public override void HandleUse()
    {
        PlantingManager.CreatePlant(plantPrefab);
        IncrementPacketCount(-1);
    }

    public void IncrementPacketCount(int increment)
    {
        SetPacketCount(packetCount + increment);
    }

    public void SetPacketCount(int value)
    {
        packetCount = value;
        itemSlot.SetCounterLabel(packetCount);
        itemSlot.SetSlotActive(packetCount > 0);

    }
}
