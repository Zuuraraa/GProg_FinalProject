using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void UpdateValue(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.transform.position + offset;

    }
}
