using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/Plant Data")]
public class PlantData : ScriptableObject
{
    public Sprite sprite;
    public int lifespan = 10;
    public PlantLoop actionLoop;
}
