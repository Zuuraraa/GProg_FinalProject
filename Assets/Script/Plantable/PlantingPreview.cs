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
            Vector3 playerPos = Player.instance.transform.position;
            graphic.sprite = ((SeedPacket)PlayerAction.currentItem).plantData.sprite;
            transform.position = new Vector3(Mathf.Round(playerPos.x), Mathf.Round(playerPos.y) + .5f, 0);
        }
    }


}
