using UnityEngine;
using UnityEngine.EventSystems;

public class XPBar : StatusBar, IPointerClickHandler
{

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip levelUpSound;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickTask();
    }

    public void OnClickTask()
    {
        Player player = Player.instance;
        //Debug.Log(string.Format("{0}/{1}", player.xp, ((PlayerStatistics)(player.stats)).xpTresholds[player.level]));
        if (player.xp >= ((PlayerStatistics)(player.stats)).xpTresholds[player.level])
        {
            if (audioSource != null && levelUpSound != null)
            {
                audioSource.PlayOneShot(levelUpSound);
            }
            
            LevelUpPanel.instance.gameObject.SetActive(true);
        }


    }
}
