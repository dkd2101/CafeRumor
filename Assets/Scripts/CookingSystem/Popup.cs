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

    private void Start()
    {
        popup.SetActive(false);
        direction.SetActive(false);
        recipeButton.onClick.AddListener(ShowRecipe);
    }

    private void Awake()
    {
        popup = GameObject.Find("Popup");
        popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
        button = popup.GetComponentInChildren<Button>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        direction = GameObject.Find("Directions");
        recipe = GameObject.Find("Recipe");
        recipeButton = recipe.GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (direction.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject())
            {
                direction.SetActive(false);
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
