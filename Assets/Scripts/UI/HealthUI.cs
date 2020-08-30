using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;

    public void SetHealth(Health newHealth)
    {
        health = newHealth;
        health.OnHealthUpdate += HealthUpdate;
        slider.minValue = 0;
        slider.maxValue = health.MaxHealth;
        HealthUpdate();
    }
    
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
