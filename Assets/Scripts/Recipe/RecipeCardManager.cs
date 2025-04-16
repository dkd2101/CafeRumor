using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardManager : MonoBehaviour
{
    public static RecipeCardManager Instance { get; private set; }
    [SerializeField] private GameObject recipeCardPrefab;
    [SerializeField] private GameObject selectionMenu;
    [SerializeField] private GameObject cardZone;
    [SerializeField] private float offsetPlacement = 150;
    [SerializeField] private List<RecipeSO> collectedRecipes;
    [SerializeField] private GameObject fadeImage;
    [SerializeField] private Image recipeDisplay;
    [SerializeField] private RectTransform onDeckZone;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button selectRecipe;
    [SerializeField] private Button backButton;

    public CardBehavior selectedCard;

    // instantiates all of the recipe cards in a row
    void Start()
    {
        fadeImage = GameObject.Find("/Canvas/FadeImage");
        if (collectedRecipes == null || collectedRecipes.Count == 0)
        {
            collectedRecipes = InventorySystem.Instance.GetCollectedRecipes();
        }

        // get the anchor point of the card zone
        float xPos = cardZone.GetComponent<RectTransform>().anchoredPosition.x;

        // instantiate recipe cards up to the amount of recipes we should have and align them 
        // extending the offset by the increment passed in the inspector
        for (int i = 0; i < collectedRecipes.Count; i++)
        {
            GameObject recipeCard = Instantiate(recipeCardPrefab, cardZone.transform);
            recipeCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, cardZone.GetComponent<RectTransform>().anchoredPosition.y);

            CardBehavior recipeCardBehavior = recipeCard.GetComponent<CardBehavior>();
            recipeCardBehavior.SetRecipeTitle(collectedRecipes[i].name);
            recipeCardBehavior.SetRecipeData(collectedRecipes[i]);
            recipeCardBehavior.SetDisplayProperties(fadeImage, onDeckZone, recipeDisplay, this.description, this);

            xPos += offsetPlacement;
        }

        selectionMenu.SetActive(false);
        selectRecipe.onClick.AddListener(SelectCurrentRecipe);

    }


    void Update()
    {
        collectedRecipes = InventorySystem.Instance.GetCollectedRecipes();
    }

    public void AddRecipe(RecipeSO recipe)
    {
        if (collectedRecipes.Contains(recipe)) return;

        collectedRecipes.Add(recipe);

        float xPos = offsetPlacement * (collectedRecipes.Count - 1);

        GameObject recipeCard = Instantiate(recipeCardPrefab, cardZone.transform);
        recipeCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, cardZone.GetComponent<RectTransform>().anchoredPosition.y);

        CardBehavior recipeCardBehavior = recipeCard.GetComponent<CardBehavior>();
        recipeCardBehavior.SetRecipeTitle(recipe.name);
        recipeCardBehavior.SetRecipeData(recipe);
        recipeCardBehavior.SetDisplayProperties(fadeImage, onDeckZone, recipeDisplay, this.description, this);
    }

    public void SetCurrCard(CardBehavior card) {
        this.selectedCard = card;
        if(card == null) {
            this.recipeDisplay.gameObject.SetActive(false);
            this.description.gameObject.SetActive(false);
            this.selectRecipe.gameObject.SetActive(false);
        } else {
            this.recipeCardPrefab.gameObject.SetActive(true);
            this.description.gameObject.SetActive(true);
            this.selectRecipe.gameObject.SetActive(true);
        }
    }

    public void ResetSelectedCard() {
        if(selectedCard != null) {
            selectedCard.BackToHand();
        } 
    }

    public void SelectCurrentRecipe() {
        this.selectedCard.LoadCookingScene();
    }

    public void ExitMenu() {
        selectionMenu.SetActive(false);
    }
}
