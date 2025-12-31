using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantData plantData;

    [Header("References")]
    [SerializeField] SpriteRenderer graphic;
    [SerializeField] PlantAction action;
    int lifespan = 10;

    private void Awake()
    {
        lifespan = plantData.lifespan;
    }

    public IEnumerator ActionLoop()
    {
        while (lifespan > 0)
        {
            action.DoAction();
            yield return new WaitForSeconds(1);
            lifespan--;
        }
        PlantingManager.plantCoords.Remove(transform.position);
        Destroy(gameObject);
    }

    public void SetYIndex(int yIndex)
    {
        graphic.sortingOrder = yIndex;
    }
}
