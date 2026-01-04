using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
                int weaponIndex = (int)levelUpType - 2;
                Weapon weapon = Player.instance.GetItem(weaponIndex) as Weapon;
                WeaponData weaponInfo = weapon.info;
                if (weapon.unlocked)
                {
                    weapon.SetLevel(weapon.level + 1);
                } else
                {
                    weapon.unlocked = true;
                    InventoryPanel.instance.itemSlots[weaponIndex].SetSlotActive(true);
                }
                break;

        }
        player.level += 1;
        player.CheckLevelUp();
        player.xpBar.UpdateValue(player.xp, ((PlayerStatistics)(player.stats)).xpTresholds[player.level]);
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
        Weapon weapon = (Player.instance.GetItem(weaponIndex)) as Weapon;
        WeaponData weaponInfo = weapon.info;

        LevelUpInfo weaponLevelUpInfo = weapon.unlocked ? weaponUpgradeInfo : weaponUnlockInfo;
        titleLabel.text = string.Format(weaponLevelUpInfo.upgradeName, weapon.name);
        descriptionLabel.text = weapon.unlocked ?
            string.Format(weaponLevelUpInfo .description, weaponInfo.damageByLevel[weapon.level])
            :
            string.Format(weaponLevelUpInfo .description, weapon.name, weaponInfo.description);
        graphic.sprite = weaponInfo.sprite;

    }


}
