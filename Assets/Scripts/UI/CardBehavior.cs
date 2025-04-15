using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text recipeTitle;
    [SerializeField] private float yOffset = 10;
    private GameObject fadeImage;
    private Image recipeDisplay;
    private RectTransform onDeckZone;
    private RectTransform rectTransform;
    private TMP_Text displayText;
    private float startingYPos;
    private float startingXPos;
    private float curYPos;
    private float curXPos;
    private float targetYPos;
    private float targetXPos;
    private RecipeSO recipe;
    private RecipeCardManager manager;

    // Start is called before the first frame update
    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        startingYPos = rectTransform.position.y;
        startingXPos = rectTransform.position.x;
        curYPos = rectTransform.position.y;
        curXPos = rectTransform.position.x;
        targetYPos = rectTransform.position.y;
        targetXPos = rectTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (curYPos != targetYPos)
        {
            curYPos = Mathf.MoveTowards(curYPos, targetYPos, 300.0f * Time.deltaTime);
            if (Math.Abs(curYPos - targetYPos) <= 0.01)
            {
                curYPos = targetYPos;
            }
            rectTransform.position = new Vector2(curXPos, curYPos);
        }

        if (curXPos != targetXPos)
        {
            curXPos = Mathf.MoveTowards(curXPos, targetXPos, 300.0f * Time.deltaTime);
            if (Math.Abs(curXPos - targetXPos) <= 0.01)
            {
                curXPos = targetXPos;
            }
            rectTransform.position = new Vector2(curXPos, curYPos);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this == this.manager.selectedCard)
            return;
        targetYPos = startingYPos + yOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this == this.manager.selectedCard)
            return;
        Debug.Log("pointer exit");
        targetYPos = startingYPos;
    }

    public void SetRecipeTitle(string recipeName)
    {
        recipeTitle.text = recipeName;
    }

    public void SetRecipeData(RecipeSO recipe)
    {
        this.recipe = recipe;
    }

    public void SetDisplayProperties(GameObject image, RectTransform deck, Image display, TMP_Text description, RecipeCardManager manager)
    {
        this.fadeImage = image;
        this.onDeckZone = deck;
        this.recipeDisplay = display;
        this.manager = manager;
        this.displayText = description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this != this.manager.selectedCard)
        {
            Debug.Log("setting to selected");
            this.manager.ResetSelectedCard();
            this.manager.SetCurrCard(this);
            Debug.Log("Previous target x: " + this.targetXPos);
            targetXPos = this.onDeckZone.position.x;
            targetYPos = this.onDeckZone.position.y;
            Debug.Log("Current target x = " + this.targetXPos);
            this.recipeDisplay.gameObject.SetActive(true);
            this.displayText.gameObject.SetActive(true);
            this.displayText.text = this.recipe.description;
            this.recipeDisplay.sprite = this.recipe.finishedImage;
        } else {
            this.BackToHand();
        }
    }

    public void BackToHand()
    {
        Debug.Log("Deselecting this card");
        this.manager.SetCurrCard(null);
        this.targetXPos = startingXPos;
        this.targetYPos = startingYPos;
    }

    public void LoadCookingScene()
    {
        fadeImage.SetActive(true);
        fadeImage.GetComponent<Fader>().StartFadeIn();
        SceneManager.LoadScene(recipe.cookingSceneName);
    }
}
