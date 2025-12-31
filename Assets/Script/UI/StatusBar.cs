using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI label;

    public virtual void UpdateValue(float value, float maxValue)
    {
        slider.value = Mathf.Clamp(value / maxValue, 0, 1);
    }
}
