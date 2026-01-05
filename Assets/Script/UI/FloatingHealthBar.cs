using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : HealthBar
{
    [SerializeField] GameObject target;
    public Vector3 offset;
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.transform.position + offset;

    }
}
