using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;


public class TimeManager : MonoBehaviour
{
    [SerializeField] private DayOfWeek dayOfWeek = DayOfWeek.FIRST_DAY;
    [SerializeField] private PartOfDay partOfDay = PartOfDay.DAYTIME;
    [SerializeField] private TimeOfDay timeOfDay = new TimeOfDay();

    public DayOfWeek DayOfWeek { get => dayOfWeek; private set => dayOfWeek = value; }
    public PartOfDay PartOfDay { get => partOfDay; private set => partOfDay = value; }
    public TimeOfDay TimeOfDay { get => timeOfDay; private set => timeOfDay = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay.addTime(Time.deltaTime);
        Debug.Log(timeOfDay.getTimeAsString());
    }
}
