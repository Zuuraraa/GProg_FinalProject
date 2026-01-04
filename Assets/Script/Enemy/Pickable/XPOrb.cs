public class XPOrb : Pickable
{
    public int value = 1;

    protected override void CollisionResult(Player player)
    {
        player.GainXp(value);
    }


    public override void Reset()
    {
        base.Reset();
        value = 1;
    }

}
