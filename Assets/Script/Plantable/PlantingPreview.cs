using UnityEditor;
using UnityEngine;

public class PlantingPreview : MonoBehaviour
{

    [SerializeField] SpriteRenderer graphic;
    void Update()
    {
        bool holdingSeedPacket = PlayerAction.currentItem.GetType() == typeof(SeedPacket);
        bool showHighlight = holdingSeedPacket && PlantingManager.canPlant;
        graphic.enabled = showHighlight;
        if (showHighlight)
        {
            transform.position = Player.instance.transform.position;
        }
    }
}
