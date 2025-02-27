using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{

    [SerializeField] private float currentAlpha = 0.0f;
    [SerializeField] private float targetAlpha = 0.0f;
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        targetAlpha = currentAlpha;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAlpha != targetAlpha) {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, 1.0f * Time.deltaTime);
            if(Math.Abs(currentAlpha - targetAlpha) <= 0.01) {
                currentAlpha = targetAlpha;
            } 
            canvasGroup.alpha = currentAlpha;
        }
    }

    public void StartFadeIn()
    {
        targetAlpha = 1.0f;
    }
}
