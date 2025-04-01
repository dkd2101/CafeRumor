using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RecipeCardManager : MonoBehaviour
{

    [SerializeField] private GameObject recipeCardPrefab;
    [SerializeField] private GameObject cardZone;
    [SerializeField] private float offsetPlacement = 150;
    [SerializeField] private List<RecipeSO> collectedRecipes;
    [SerializeField] private GameObject fadeImage;

    // instantiates all of the recipe cards in a row
    void Start()
    {
        // get the anchor point of the card zone
        float xPos = cardZone.GetComponent<RectTransform>().anchoredPosition.x;

        // instantiate recipe cards up to the amount of recipes we should have and align them 
        // extending the offset by the increment passed in the inspector
        for (int i = 0; i < collectedRecipes.Count; i++)
        {
            GameObject recipeCard = Instantiate(recipeCardPrefab, cardZone.transform);
            recipeCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(cardZone.GetComponent<RectTransform>().anchoredPosition.x + xPos, cardZone.GetComponent<RectTransform>().anchoredPosition.y);
            recipeCard.GetComponent<CardBehavior>().SetRecipeTitle(collectedRecipes[i].name);
            CardBehavior recipeCardBehavior = recipeCard.GetComponent<CardBehavior>();
            recipeCardBehavior.SetRecipeData(collectedRecipes[i]);
            recipeCardBehavior.SetFadeImage(fadeImage);
            xPos += offsetPlacement;
        }
    }

    public void AddRecipe(RecipeSO recipe) {
        collectedRecipes.Add(recipe);
    }
}
