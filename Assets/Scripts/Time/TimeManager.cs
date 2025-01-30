using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimeOfDay timeOfDay = new TimeOfDay();
    [SerializeField] private float timeMultiplier = 5f;
    
    public TimeOfDay TimeOfDay { get => timeOfDay; private set => timeOfDay = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay.addTime(Time.deltaTime * timeMultiplier);
        Debug.Log(timeOfDay.getTimeAsString("dateTime"));
    }

    private void OnDestroy()
    {
        //TODO: this will be where we save.
    }
}
