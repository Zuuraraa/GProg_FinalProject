using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateValue(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }

}
