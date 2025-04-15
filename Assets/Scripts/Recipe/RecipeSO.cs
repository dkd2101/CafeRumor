using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Recipes/RecipeObject")]
public class RecipeSO : ScriptableObject
{
    public string dishName;
    public string[] ingredients;

    public string[] instructions;

    public string cookingSceneName;

    public Sprite finishedImage;

    public string description;
}
