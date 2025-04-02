using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using TMPro;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimeOfDay timeOfDay = new TimeOfDay();
    [SerializeField] private float timeMultiplier = 5f;
    [SerializeField] private int UIMinuteUpdateCount = 15;
    [SerializeField] private TMP_Text timeText;
    private int currentMinuteMod;

    private bool pauseTime;

    public TimeOfDay TimeOfDay { get => timeOfDay; private set => timeOfDay = value; }

    // Start is called before the first frame update
    void Start()
    {
        currentMinuteMod = timeOfDay.Minutes % (60 / UIMinuteUpdateCount);
        pauseTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseTime)
        {
            timeOfDay.addTime(Time.deltaTime * timeMultiplier);
            if (currentMinuteMod != timeOfDay.Minutes % (60 / UIMinuteUpdateCount))
            {
                currentMinuteMod = timeOfDay.Minutes % (60 / UIMinuteUpdateCount);
                timeText.text = timeOfDay.getTimeAsString("UIFormatting", UIMinuteUpdateCount);
            }
        }
    }

    public void GoToNextDay()
    {
        if (this.timeOfDay.DayOfWeek == GameTime.DayOfWeek.FIRST_DAY && this.timeOfDay.PartOfDay == PartOfDay.DAYTIME)
        {
            this.timeOfDay.setTime(GameTime.DayOfWeek.FIRST_DAY, 19, 0, 0);
        }
        else if (timeOfDay.DayOfWeek == GameTime.DayOfWeek.FIRST_DAY && timeOfDay.PartOfDay == PartOfDay.NIGHTTIME)
        {
            this.timeOfDay.setTime(GameTime.DayOfWeek.SECOND_DAY, 8, 0, 0);
        }
        else if (timeOfDay.DayOfWeek == GameTime.DayOfWeek.SECOND_DAY && timeOfDay.PartOfDay == PartOfDay.DAYTIME)
        {
            this.timeOfDay.setTime(GameTime.DayOfWeek.SECOND_DAY, 19, 0, 0);
        }
        else if (timeOfDay.DayOfWeek == GameTime.DayOfWeek.SECOND_DAY && timeOfDay.PartOfDay == PartOfDay.NIGHTTIME)
        {
            this.timeOfDay.setTime(GameTime.DayOfWeek.THRID_DAY, 8, 0, 0);
        }
        else if (timeOfDay.DayOfWeek == GameTime.DayOfWeek.THRID_DAY && timeOfDay.PartOfDay == PartOfDay.DAYTIME)
        {
            this.timeOfDay.setTime(GameTime.DayOfWeek.THRID_DAY, 18, 0, 0);
        }
        else if (timeOfDay.DayOfWeek == GameTime.DayOfWeek.THRID_DAY && timeOfDay.PartOfDay == PartOfDay.NIGHTTIME)
        {
            ResetLoop();
        }

    }

    public void PauseTime(bool isPaused) {
        pauseTime = isPaused;
    }

    private void ResetLoop()
    {
        this.timeOfDay.setTime(GameTime.DayOfWeek.FIRST_DAY, 8, 0, 0);
    }

    private void OnDestroy()
    {
        //TODO: this will be where we save.
    }
}
