using UnityEngine;


public enum LevelUpType { None = -1, HP, Speed, Sickle, Spade, WateringCan }


public class LevelUpPanel : MonoBehaviour
{

    public static LevelUpPanel instance;
    public static bool hasLevelUpOptions;

    [SerializeField] LevelUpCard[] levelUpCards = new LevelUpCard[2];
    
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void LeveledUp()
    {
        if (!hasLevelUpOptions)
        {
            hasLevelUpOptions = true;
            GenerateLevelUpOptions();
        }
    }

    static void GenerateLevelUpOptions()
    {
        LevelUpType firstOption = GenerateLevelUpOption(0);
        GenerateLevelUpOption(1, firstOption);
    }

    static LevelUpType GenerateLevelUpOption(int index, LevelUpType ignore = LevelUpType.None)
    {
        LevelUpType upgradeType;
        do
        {
            upgradeType = (LevelUpType)Random.Range(0, 5);
        } while (upgradeType == ignore || !CheckIfWeaponIsMaxLevel(upgradeType));

        instance.levelUpCards[index].SetLevelUpType(upgradeType);
        return upgradeType;
    }

    static bool CheckIfWeaponIsMaxLevel(LevelUpType upgradeType)
    {
        if ((int)upgradeType >= 2)
        {
            return ((Weapon)(Player.instance.GetItem((int)upgradeType - 2))).level < 3;
        }
        else
        {
            return false;
        }
    }
}
