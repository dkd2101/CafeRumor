using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CreditsManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField] private Fader fader;
    [SerializeField] private GameObject[] buttonObjs;

    private bool hasPlayed = false;
    private bool creditsFadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CreditsVideo.mp4");
        
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fader.isVisible && !hasPlayed)
        {
            videoPlayer.Play();
            hasPlayed = true;
        }
    }
    
    void OnVideoFinished(VideoPlayer vp)
    {
        if (!creditsFadeIn)
        {
            Debug.Log("Video finished, starting fade in.");
            fader.StartFadeIn();
            creditsFadeIn = true;
            foreach (var obj in buttonObjs)
            {
                obj.SetActive(true);
            }
        }
    }

    public void GoToMainMenu()
    {
        GameStateManager.getInstance().resetGameData();
        SceneManager.LoadScene("TitleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
