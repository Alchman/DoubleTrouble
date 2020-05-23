using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider slider;

    void Start()
    {
        health.OnHealthUpdate += HealthUpdate;
        slider.minValue = 0;
        slider.maxValue = health.MaxHealth;
        HealthUpdate();
    }

    private void HealthUpdate()
    {
        slider.value = health.HealthLeft;
    }
}
