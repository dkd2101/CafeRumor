using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<RecipeStage> stages;
    public List<string> finalProducts;
}
