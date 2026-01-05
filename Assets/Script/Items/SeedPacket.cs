using System;
using System.Collections;
using UnityEngine;

public class SeedPacket : Item
{
    public GameObject plantPrefab;
    [SerializeField] int packetCount = 0;


    public override void CheckUse(AudioSource source)
    {
        if (Input.GetMouseButtonDown(0) && PlantingManager.IsCurrentPositionClear() && PlantingManager.canPlant && packetCount > 0) 
        { 
            if (source != null && useSound != null)
            {
                source.pitch = UnityEngine.Random.Range(0.9f, 1.1f); // Variasi nada dikit
                source.PlayOneShot(useSound);
            }
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
        if (Player.instance != null && Player.instance.animator != null)
        {
            Player.instance.animator.SetTrigger("Plant");
        }
        
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
        itemSlot.SetSlotUnlocked(packetCount > 0);

    }
}
