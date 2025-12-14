
using UnityEngine;

public class Aggression : MonoBehaviour
{
    public bool hasAggro = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasAggro = false;
        }
    }

}
