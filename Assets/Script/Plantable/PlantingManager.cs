using System.Collections.Generic;
using UnityEngine;

public class PlantingManager : MonoBehaviour
{

    public static PlantingManager instance;
    public static bool canPlant = false;
    public static Vector3 currentPlantingPosition;
    public static List<Vector3> plantCoords = new List<Vector3>();

    [Header("Outer Bounds")]
    public Vector2 outerMinPosition;
    public Vector2 outerMaxPosition;

    [Header("Inner Bounds")]
    public Vector2 innerMinPosition;
    public Vector2 innerMaxPosition;

    [Header("References")]
    [SerializeField] GameObject plantPrefab;
    [SerializeField] GameObject preview;



    private void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 position = Player.instance.transform.position;
        bool insideOuterBox = IsWithinRange(position.x, outerMinPosition.x, outerMaxPosition.x) && IsWithinRange(position.y, outerMinPosition.y, outerMaxPosition.y);
        bool outsideInnerBox = !(IsWithinRange(position.x, innerMinPosition.x, innerMaxPosition.x) && IsWithinRange(position.y, innerMinPosition.y, innerMaxPosition.y));
        canPlant = insideOuterBox && outsideInnerBox;
    }

    bool IsWithinRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    public static void CreatePlant(GameObject prefab)
    {
        GameObject newPlant = Instantiate(prefab);
        newPlant.transform.position = instance.preview.transform.position;
        plantCoords.Add(newPlant.transform.position);

        Plant plantScript = newPlant.GetComponent<Plant>();
        plantScript.SetYIndex((int)(newPlant.transform.position.y));
        plantScript.StartCoroutine(plantScript.ActionLoop());
    }

    public static bool IsCurrentPositionClear()
    {
        return !plantCoords.Contains(currentPlantingPosition);
    }


    public static void SetPreviewImage(Sprite sprite)
    {
        instance.preview.GetComponent<PlantingPreview>().graphic.sprite = sprite;
    }
}
