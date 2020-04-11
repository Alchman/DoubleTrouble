using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        health.OnHealthUpdate += HealthUpdate;
        slider.minValue = 0;
        slider.maxValue = health.MaxHealth;
    }

    private void HealthUpdate()
    {
        slider.value = health.HealthLeft;
    }

}
