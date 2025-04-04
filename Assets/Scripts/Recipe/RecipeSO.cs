using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Recipes/RecipeObject")]
public class RecipeSO : ScriptableObject
{
    public string dishName;
    public string[] ingredients;

    public string[] instructions;

    public string cookingSceneName;
}
