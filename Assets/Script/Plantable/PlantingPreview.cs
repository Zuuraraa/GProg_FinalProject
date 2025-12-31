using UnityEditor;
using UnityEngine;

public class PlantingPreview : MonoBehaviour
{

    public SpriteRenderer graphic;
    void Update()
    {
        bool holdingSeedPacket = PlayerAction.currentItem.GetType() == typeof(SeedPacket);
        bool showHighlight = holdingSeedPacket && PlantingManager.canPlant;
        graphic.enabled = showHighlight;
        if (showHighlight)
        {
            Vector3 playerPos = Player.instance.transform.position;
            transform.position = new Vector3(Mathf.Round(playerPos.x), Mathf.Round(playerPos.y) + .5f, 0);
            PlantingManager.currentPlantingPosition = transform.position;
        }
    }    

}
