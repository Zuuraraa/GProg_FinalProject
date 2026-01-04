using UnityEngine;

public class XPBar : StatusBar
{
    public override void UpdateValue(float value, float maxValue)
    {
        base.UpdateValue(value, maxValue);
        if (label != null)
        {
            if (value >= maxValue)
            {
                label.text = "LEVEL UP!";
            }
            else
            {
                label.text = value.ToString() + " / " + maxValue.ToString();
            }
        }
    }

    public void ReachedMaxXP()
    {
        slider.value = 1;
        label.text = "MAX LEVEL";
    }

    public void OnClickTask()
    {
        Player player = Player.instance;
        //Debug.Log(string.Format("{0}/{1}", player.xp, ((PlayerStatistics)(player.stats)).xpTresholds[player.level]));
        if (player.xp >= ((PlayerStatistics)(player.stats)).xpTresholds[player.level])
        {
            LevelUpPanel.instance.gameObject.SetActive(true);
        }


    }
}
