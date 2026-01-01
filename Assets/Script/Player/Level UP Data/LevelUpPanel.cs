using UnityEngine;

public class LevelUpPanel : MonoBehaviour
{
    enum UpgradeTypes { HP, BaseHP, Speed, Sickle, Spade, WateringCan }

    public LevelUpCard[] levelUpCard = new LevelUpCard[2];
    public LevelData levelData = new LevelData();
}


[System.Serializable] 
public class LevelData
{
    public int hpLevel = 0, baseHpLevel = 0, speedLevel = 0, sickleLevel = 0, spadeLevel = -1, wateringCanLevel = -1;
}
