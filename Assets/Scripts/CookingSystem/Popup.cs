using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    private GameObject popup;
    private TextMeshProUGUI popupText;
    private Button button;
    private TextMeshProUGUI buttonText;

    private GameObject direction;
    private GameObject recipe;
    private Button recipeButton;
    private GameObject panel;
    private GameObject winPanel;
    private Button continueButton;

    private void Start()
    {
        winPanel.SetActive(false);
        popup.SetActive(false);
        if (direction != null)
        {
            direction.SetActive(false);
            recipeButton.onClick.AddListener(ShowRecipe);
        }
    }

    private void Awake()
    {
        popup = GameObject.Find("Popup");
        panel = GameObject.Find("Popup/Panel");
        winPanel = GameObject.Find("Popup/WinBackdrop");
        continueButton = GameObject.Find("Popup/WinBackdrop/Continue").GetComponent<Button>();
        if (popup != null)
        {
            popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
            button = popup.GetComponentInChildren<Button>();
            buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            UnityEngine.Debug.Log("popup is found");
        }
        direction = GameObject.Find("Directions");
        recipe = GameObject.Find("Recipe");
        if (recipe != null)
        {
            recipeButton = recipe.GetComponentInChildren<Button>();
        }

        continueButton.onClick.AddListener(LoadOriginalScene);
    }

    private void Update()
    {
        if (direction != null)
        {
            if (direction.activeSelf && Input.GetMouseButtonDown(0))
            {
                if (!IsPointerOverUIObject())
                {
                    direction.SetActive(false);
                }
            }
        }

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
        popupText.fontSize = 20;
        popupText.rectTransform.sizeDelta = new Vector2(390, 50);
        buttonText.text = "Continue!";
        buttonText.fontSize = 16;
        popup.SetActive(true);
        panel.SetActive(false);
        winPanel.SetActive(true);
        button.onClick.AddListener(LoadOriginalScene);
    }

    public void ShowRecipe()
    {
        direction.SetActive(!direction.activeSelf);
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
