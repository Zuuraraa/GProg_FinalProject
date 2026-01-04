public class HealthPickup : Pickable
{
    int value = 10;
    protected override void CollisionResult(Player player)
    {
        player.TakeDamage(-value);
    }

    public override void Reset()
    {
        base.Reset();
        value = 10;
    }

}
