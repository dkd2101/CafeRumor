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
    
    public TimeOfDay TimeOfDay { get => timeOfDay; private set => timeOfDay = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        currentMinuteMod = timeOfDay.Minutes % (60 / UIMinuteUpdateCount);
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay.addTime(Time.deltaTime * timeMultiplier);
        if (currentMinuteMod != timeOfDay.Minutes % (60 / UIMinuteUpdateCount))
        {
            currentMinuteMod = timeOfDay.Minutes % (60 / UIMinuteUpdateCount);
            timeText.text = timeOfDay.getTimeAsString("UIFormatting", UIMinuteUpdateCount);
        }
    }

    private void OnDestroy()
    {
        //TODO: this will be where we save.
    }
}
