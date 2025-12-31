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
        packetCount--;
    }
}
