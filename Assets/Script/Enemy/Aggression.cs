using Pathfinding;
using UnityEngine;

public class Aggression : MonoBehaviour
{
    [SerializeField] AIDestinationSetter destination;

    public void GainAggro()
    {
        destination.target = Player.instance.transform;
    }

    void LoseAggro()
    {
        destination.target = null;
        enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoseAggro();
        }
    }

}
