using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpCard : MonoBehaviour
{
    [SerializeField] PlayerStatistics playerStats;
    [SerializeField] WeaponData[] weaponData = new WeaponData[3];

    [Header("References")]
    [SerializeField] TextMeshProUGUI titleLabel;
    [SerializeField] TextMeshProUGUI descriptionLabel;
    [SerializeField] Image graphic;


    [Header("Level Up Info")]
    [SerializeField] LevelUpInfo hpInfo;
    [SerializeField] LevelUpInfo speedInfo;
    [SerializeField] LevelUpInfo weaponUpgradeInfo;
    [SerializeField] LevelUpInfo weaponUnlockInfo;

    LevelUpType levelUpType;

    public void SetLevelUpType(LevelUpType value)
    {
        levelUpType = value;
        switch (levelUpType) {
            case LevelUpType.HP:
                HpLevelUp();
                break;
            case LevelUpType.Speed:
                SpeedLevelUp();
                break;
            default:
                WeaponLevelUp();
                break;
        }

    }

    public void OnClick()
    {
        Player player = Player.instance;
        switch (levelUpType)
        {
            case LevelUpType.HP:
                player.maxHP += playerStats.hpPerLevel;
                player.TakeDamage(-playerStats.hpPerLevel);
                player.healthBar.UpdateValue(player.currentHP, player.maxHP);
                break;
            case LevelUpType.Speed:
                player.speedMult += playerStats.speedMultPerLevel;
                break;
            default:
                break;

        }
        player.level += 1;
        player.CheckLevelUp();
        LevelUpPanel.hasLevelUpOptions = false;
        LevelUpPanel.instance.gameObject.SetActive(false);
    }

    void HpLevelUp()
    {
        titleLabel.text = hpInfo.upgradeName;
        descriptionLabel.text = string.Format(hpInfo.description, playerStats.hpPerLevel);
        graphic.sprite = hpInfo.image;
    }

    void SpeedLevelUp()
    {
        titleLabel.text = speedInfo.upgradeName;
        descriptionLabel.text = string.Format(speedInfo.description, (int)(playerStats.speedMultPerLevel * 100));
        graphic.sprite = speedInfo.image;
    }

    void WeaponLevelUp()
    {
        int weaponIndex = (int)levelUpType - 2;
        WeaponData weapon = weaponData[weaponIndex];

        LevelData levelData = LevelUpPanel.GetLevelData();
        LevelUpInfo weaponInfo = levelData.weaponLevel[weaponIndex] >= 0 ? weaponUpgradeInfo : weaponUnlockInfo;
        titleLabel.text = string.Format(weaponInfo.upgradeName, weapon.name);
        descriptionLabel.text = levelData.weaponLevel[weaponIndex] >= 0 ?
            string.Format(weaponInfo.description, weapon.damageByLevel[levelData.weaponLevel[weaponIndex]])
            :
            string.Format(weaponInfo.description, weapon.name, weapon.description);
        graphic.sprite = weapon.sprite;

    }


}
