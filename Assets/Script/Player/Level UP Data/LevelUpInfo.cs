using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpInfo", menuName = "Scriptable Objects/Level Up Info")]
public class LevelUpInfo : ScriptableObject
{
    public string upgradeName;
    public Sprite image;
    [TextArea] public string description;
}
