using UnityEngine;

public class XPOrb : Pickable
{
    public int value = 1;

    [Header("Audio Settings")]
    public AudioClip pickupSound;

    protected override void CollisionResult(Player player)
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.5f);
        }
        player.GainXp(value);
    }


    public override void Reset()
    {
        base.Reset();
        value = 1;
    }

}
