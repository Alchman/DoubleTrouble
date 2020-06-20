using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider slider;

    void Start()
    {
        if (health == null)
        {
            Debug.LogWarning("Health object is not set!");
            return;
        }
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
