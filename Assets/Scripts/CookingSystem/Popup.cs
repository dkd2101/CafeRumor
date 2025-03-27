using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Popup : MonoBehaviour
{
    private GameObject popup;
    private TextMeshProUGUI popupText;
    private Button button;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        popup.SetActive(false);
    }

    private void Awake()
    {
        popup = GameObject.Find("Popup");
        popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
        button = popup.GetComponentInChildren<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowErrorPopup()
    {
        popup.SetActive(true);
        button.onClick.AddListener(ReloadScene);
    }

    private void LoadOriginalScene()
    {
        SceneManager.LoadScene("DogOnHighway");
    }

    public void ShowWinPopup(string text)
    {
        button.onClick.RemoveAllListeners();
        popupText.text = text;
        popupText.fontSize = 25;
        popupText.rectTransform.sizeDelta = new Vector2(370, 50);
        buttonText.text = "Continue!";
        buttonText.fontSize = 20;
        popup.SetActive(true);
        button.onClick.AddListener(LoadOriginalScene);
    }
}
