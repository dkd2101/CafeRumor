using System;
using Unity.Mathematics;
using UnityEngine;

namespace GameTime
{
    public enum DayOfWeek
    {
        FIRST_DAY,
        SECOND_DAY,
        THRID_DAY,
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
        private int startOfDay = 6;
        private int endOfDay = 18;

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

            if (this.partOfDay != PartOfDay.DAYTIME && hours is < 18 and >= 6)
            {
                this.partOfDay = PartOfDay.DAYTIME;
            } else if (this.partOfDay != PartOfDay.NIGHTTIME && hours is < 6 or >= 18)
            {
                this.partOfDay = PartOfDay.NIGHTTIME;
            }

            hours %= 24;
        }
        
        public void setTime(int hours = 0, int minutes = 0, float seconds = 0)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public string getTimeAsString()
        {
            return $"{hours:D2}:{minutes:D2}:{(int)seconds:D2}";
        }
    }
}