using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeCardManager : MonoBehaviour
{

    [SerializeField] private GameObject recipeCardPrefab;
    [SerializeField] private GameObject selectionMenu;
    [SerializeField] private GameObject cardZone;
    [SerializeField] private float offsetPlacement = 150;
    [SerializeField] private List<RecipeSO> collectedRecipes;
    [SerializeField] private GameObject fadeImage;

    // instantiates all of the recipe cards in a row
    void Start()
    {
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
            recipeCardBehavior.SetFadeImage(fadeImage);

            xPos += offsetPlacement;
        }

        selectionMenu.SetActive(false);

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
        recipeCardBehavior.SetFadeImage(fadeImage);
    }
}
