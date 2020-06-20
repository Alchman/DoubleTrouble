using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : GenericSingletonClass<DayNight>{
    // [SerializeField] private Text TimeUi;
    [SerializeField] private int DayDuration = 240; // 4 минуты
    public Action TimeChanged = delegate {};
    
    // private int sec;
    private float minuteDuration;
    private float hourDuration;
    private float timeFromStart;
    
    public int Days {get; private set;}
    public int Hours {get; private set;}
    public int Minutes {get; private set;}

    private int lastMinute;

    // Start is called before the first frame update
    void Start() {
        hourDuration = (float)DayDuration / 24;
        minuteDuration = hourDuration / 60;
    }

    // Update is called once per frame
    void Update() {

        timeFromStart += Time.deltaTime;
        Days = (int)timeFromStart / DayDuration;
        float leftOver = timeFromStart % DayDuration;
       Hours = Mathf.FloorToInt((leftOver) / hourDuration); // остаток 15
       Minutes =  Mathf.FloorToInt((leftOver % hourDuration) / minuteDuration); // остаток 5

       if(lastMinute != Minutes) {
           TimeChanged();
           lastMinute = Minutes;
       }

       // TimeUi.text = Days + ":" + Hours + ":" + Minutes;
    }
}
