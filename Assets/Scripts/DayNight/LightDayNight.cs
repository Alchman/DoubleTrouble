using UnityEngine;

public class LightDayNight : MonoBehaviour{
    [SerializeField] [Tooltip("Настроить градиент для дня и ночи")]
    private Gradient colorNightDay;

    [SerializeField] [Tooltip("Источник для света")]
    private Light directionLight;

    private void Start() {
        DayNight.Instance.TimeChanged += UpdateLightForDay;
    }

    void UpdateLightForDay() {
        directionLight.color = colorNightDay.Evaluate(DayNight.Instance.ProgressDay);
    }
}
