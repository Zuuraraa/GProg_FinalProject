using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : StatusBar
{
    public override void UpdateValue(float value, float maxValue)
    {
        base.UpdateValue(value, maxValue);
        if (label != null)
        {
            label.text = value.ToString() + " / " + maxValue.ToString();
        }
    }

}
