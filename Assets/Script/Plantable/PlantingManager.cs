using UnityEngine;

public class PlantingManager : MonoBehaviour
{

    public static PlantingManager instance;
    public static bool canPlant = false;

    [SerializeField] GameObject highlight;

    [Header("Outer Bounds")]
    public Vector2 outerMinPosition;
    public Vector2 outerMaxPosition;

    [Header("Inner Bounds")]
    public Vector2 innerMinPosition;
    public Vector2 innerMaxPosition;

    Player player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = player.transform.position;
        bool insideOuterBox = IsWithinRange(position.x, outerMinPosition.x, outerMaxPosition.x) && IsWithinRange(position.y, outerMinPosition.y, outerMaxPosition.y);
        bool outsideInnerBox = !(IsWithinRange(position.x, innerMinPosition.x, innerMaxPosition.x) && IsWithinRange(position.y, innerMinPosition.y, innerMaxPosition.y));
        canPlant = insideOuterBox && outsideInnerBox;

        highlight.SetActive(canPlant);
        if (canPlant)
        {
            highlight.transform.position = player.transform.position;
        }
    }

    bool IsWithinRange(float value, float min, float max)
    {
        return value >= min && value <= max;
    }
}
