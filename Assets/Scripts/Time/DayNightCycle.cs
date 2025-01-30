using System.Collections;
using System.Collections.Generic;
using GameTime;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    private Volume volume;
    private PartOfDay previousPartOfDay;
    [SerializeField] private float transitionSpeed = 0.01f;
    [SerializeField] private TimeManager timeManager;
    private TimeOfDay timeOfDay;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponentInChildren<Volume>();
        timeOfDay = timeManager.TimeOfDay;
        previousPartOfDay = timeOfDay.PartOfDay;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPartOfDay != timeOfDay.PartOfDay)
        {
            previousPartOfDay = timeOfDay.PartOfDay;
            if (previousPartOfDay == PartOfDay.DAYTIME)
            {
                StartCoroutine(TransitionVolumeWeight(1f));
            }
            else
            {
                StartCoroutine(TransitionVolumeWeight(0f));
            }
        }
    }

// Coroutine for smooth transition
    private IEnumerator TransitionVolumeWeight(float targetWeight)
    {
        float startWeight = volume.weight;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            volume.weight = Mathf.Lerp(startWeight, targetWeight, elapsedTime);
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        volume.weight = targetWeight; // Ensure exact final value
    }
}
