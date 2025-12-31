using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] PlantData plantData;

    [Header("References")]
    [SerializeField] SpriteRenderer graphic;

    int lifespan = 10;
    
    public IEnumerator ActionLoop()
    {
        while (lifespan > 0)
        {
            Debug.Log(lifespan);
            plantData.actionLoop.Action();
            yield return new WaitForSeconds(1);
            lifespan--;
        }
        Destroy(gameObject);
    }

    public void AssignData(PlantData data, int yIndex)
    {
        plantData = data;
        lifespan = plantData.lifespan;
        graphic.sprite = plantData.sprite;
        graphic.sortingOrder = yIndex;
    }
}
