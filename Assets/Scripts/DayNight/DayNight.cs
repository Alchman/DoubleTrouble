using System;
using UnityEngine;

public class DayNight : GenericSingletonClass<DayNight>{
    [SerializeField] [Tooltip("количество минут в дне")]
    private int DayDuration = 240; // 4 минуты
    [SerializeField][Tooltip("Начало дня в часах")] private float startDayHour;

    public Action TimeChanged = delegate {
                                };
    private float minuteDuration;
    private float hourDuration;
    private float timeFromStart;

    public int   Days        {get; private set;}
    public int   Hours       {get; private set;}
    public int   Minutes     {get; private set;}
    public float ProgressDay {get; private set;}

    private int lastMinute;
 
    void Start() {
        hourDuration   = (float) DayDuration / 24;
        minuteDuration = hourDuration        / 60;
        timeFromStart = startDayHour * hourDuration;

    }

    void Update() {
        timeFromStart += Time.deltaTime;
        Days          =  (int) timeFromStart / DayDuration;
        float leftOver = timeFromStart % DayDuration;
        Hours   = Mathf.FloorToInt((leftOver)                / hourDuration);
        Minutes = Mathf.FloorToInt((leftOver % hourDuration) / minuteDuration);

        if(lastMinute != Minutes) {
            TimeChanged();
            lastMinute = Minutes;
        }

        ProgressDay = (float) (Hours * 60 + Minutes) / 1440;
        print(timeFromStart);
    }
}
