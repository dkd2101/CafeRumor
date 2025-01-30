using System;
using Unity.Mathematics;
using UnityEngine;

namespace GameTime
{
    public enum DayOfWeek
    {
        FIRST_DAY = 0,
        SECOND_DAY = 1,
        THRID_DAY = 2,
    }

    public enum PartOfDay
    {
        DAYTIME,
        NIGHTTIME
    }
    
    [System.Serializable]
    public class TimeOfDay
    {
        [SerializeField] private DayOfWeek dayOfWeek;
        [SerializeField] private PartOfDay partOfDay;
        [SerializeField] private int hours;
        [SerializeField] private int minutes;
        [SerializeField] private float seconds;

        public DayOfWeek DayOfWeek { get => dayOfWeek; private set => dayOfWeek = value; }
        public PartOfDay PartOfDay { get => partOfDay; private set => partOfDay = value; }
        public int Hours { get => hours; private set => hours = value; }
        public int Minutes { get => minutes; private set => minutes = value; }
        public float Seconds { get => seconds; private set => seconds = value; }


        private const int startOfDay = 6;
        private const int endOfDay = 18;

        public TimeOfDay(int hours = 0, int minutes = 0, int seconds = 0)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public void addTime(float addedSeconds)
        {
            seconds += addedSeconds;
            if (seconds >= 60)
            {
                minutes += (int)(seconds / 60);
                seconds %= 60;
            }

            if (minutes >= 60)
            {
                hours += minutes / 60;
                minutes %= 60;
            }

            if (partOfDay != PartOfDay.DAYTIME && hours is < endOfDay and >= startOfDay)
            {
                partOfDay = PartOfDay.DAYTIME;
            } else if (partOfDay != PartOfDay.NIGHTTIME && hours is < startOfDay or >= endOfDay)
            {
                partOfDay = PartOfDay.NIGHTTIME;
            }

            if (hours >= 24)
            {
                dayOfWeek = (DayOfWeek)(((int)dayOfWeek + hours / 24) % 3);
                hours %= 24;
            }
        }
        
        public void setTime(DayOfWeek dayOfWeek = DayOfWeek.FIRST_DAY, int hours = 0, int minutes = 0, float seconds = 0)
        {
            this.dayOfWeek = dayOfWeek;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public string getTimeAsString(String flag = "default")
        {
            switch (flag)
            {
                case "dateTime":
                    int displayedHours = hours % 12;
                    if (displayedHours == 0)
                    {
                        displayedHours = 12;
                    }
                    return $"{displayedHours:D2}:{minutes:D2}:{(int)seconds:D2}";
                case "default":
                default:
                    return $"{hours:D2}:{minutes:D2}:{(int)seconds:D2}";
            }
        }
    }
}