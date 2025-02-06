using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour, IDroppable
{
    [SerializeField] private List<string> correctRecipe;
    private List<string> currentIngredients = new List<string>();

    public void OnDrop(Draggable ingredient)
    {
        Destroy(ingredient.gameObject);

        // Add the dropped ingredient to the current list
        currentIngredients.Add(ingredient.name);

        if (currentIngredients.Count == correctRecipe.Count)
        {
            CheckRecipeCorrectness();
        }
    }

    private void CheckRecipeCorrectness()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctRecipe.Count; i++)
        {
            if (correctRecipe[i] != currentIngredients[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("Recipe is correct!");
            currentIngredients.Clear();
        }
        else
        {
            Debug.Log("Recipe is incorrect! The correct recipe is: " + string.Join(", ", correctRecipe.ToArray()));
            Debug.Log("Your recipe was: " + string.Join(", ", currentIngredients.ToArray()));
            currentIngredients.Clear();
        }
    }
}
