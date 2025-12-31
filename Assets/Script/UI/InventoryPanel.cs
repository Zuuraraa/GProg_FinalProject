using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel instance;

    public List<ItemSlot> itemSlots;
    public List<Sprite> itemImages;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        int slotCount = itemSlots.Count;
        for (int i = 0; i < slotCount; i++)
        {
            itemSlots[i].SetHotkeyLabel(i+1);
            itemSlots[i].SetImage(itemImages[i]);
        }
    }

    public static void ActivateItemSlot(int index)
    {
        int slotCount = instance.itemSlots.Count;
        for (int i = 0; i < slotCount; i++)
        {
            instance.itemSlots[i].SetSlotSelected(i == index);
        }
    }
}
