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

    public void ReachedMaxHP()
    {
        slider.value = 1;
        label.text = "MAX LEVEL";
    }
}
