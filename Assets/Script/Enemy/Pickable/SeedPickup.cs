using System;
using UnityEngine;

public class SeedPickup : Pickable
{
    [Serializable] public enum SeedType { Corn, Chili }

    [SerializeField] SpriteRenderer seedGraphic;

    public SeedType seedType;

    [Header("Images")]
    public Sprite cornSprite;
    public Sprite chiliSprite;

    protected override void CollisionResult(Player player)
    {
        SeedPacket seedPacket = ((SeedPacket)(player.GetItem(seedType == SeedType.Corn ? 3 : 4)));
        seedPacket.IncrementPacketCount(1);
        gameObject.SetActive(false);
    }

    public void SetSeedType(SeedType type)
    {
        seedType = type;
        seedGraphic.sprite = type == SeedType.Corn ? cornSprite : chiliSprite;
    }



    public override void Reset()
    {
        base.Reset();
    }
}
