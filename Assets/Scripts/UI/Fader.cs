using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{

    [SerializeField] private float currentAlpha = 0.0f;
    [SerializeField] private float targetAlpha = 0.0f;
    [SerializeField] private float fadeSpeed = 1.0f;
    private CanvasGroup canvasGroup;
    public bool isVisible;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        currentAlpha = canvasGroup.alpha;
        targetAlpha = canvasGroup.alpha;
        isVisible = canvasGroup.alpha > 0.0f;
    }

    void Start()
    {
        this.StartFadeOut();
        SceneManager.sceneLoaded += AutoFadeOut;
    }

    private void AutoFadeOut(Scene scene, LoadSceneMode mode) {
        this.StartFadeOut();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentAlpha != targetAlpha)
        {
            Debug.Log("Fading");
            Debug.Log(this.gameObject.name);
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            if (Math.Abs(currentAlpha - targetAlpha) <= 0.01)
            {
                currentAlpha = targetAlpha;
            }
            canvasGroup.alpha = currentAlpha;
        }
    }

    public void StartFadeIn()
    {
        Debug.Log(this.gameObject.name);
        isVisible = true;
        targetAlpha = 1.0f;
        Debug.Log(targetAlpha);
    }

    public void StartFadeOut()
    {
        Debug.Log(this.gameObject.name);
        isVisible = false;
        targetAlpha = 0.0f;
        Debug.Log(targetAlpha);
    }
}
