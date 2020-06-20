using System;
using UnityEngine;
using UnityEngine.UI;

public  class TimeUi : MonoBehaviour{
    [SerializeField] private Text clock;


    private void Start() {
        DayNight.Instance.TimeChanged += UpdateTime;
    }

    void UpdateTime() {
        clock.text = DayNight.Instance.Days + ":" + DayNight.Instance.Hours + ":" + DayNight.Instance.Minutes;
    }
}
