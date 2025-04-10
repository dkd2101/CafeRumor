using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;

[CreateAssetMenu(fileName = "RecipeData", menuName = "Recipes/RecipeObject")]
public class RecipeSO : ScriptableObject
{
    public string dishName;
    public string[] ingredients;

    public string[] instructions;

    public string cookingSceneName;

    public Image finishedImage;
}
