using UnityEngine;

public class PlantingManager : MonoBehaviour
{

    public static PlantingManager instance;
    public static bool canPlant = false;

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

    public static void CreatePlant(PlantData data)
    {
        GameObject newPlant = Instantiate(instance.plantPrefab);
        Plant plantScript = newPlant.GetComponent<Plant>();
        newPlant.transform.position = instance.preview.transform.position;
        plantScript.AssignData(data, (int)(instance.outerMinPosition.y - newPlant.transform.position.y));
        plantScript.StartCoroutine(plantScript.ActionLoop());
    }
}
