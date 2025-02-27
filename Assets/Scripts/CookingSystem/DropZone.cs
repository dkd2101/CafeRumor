using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] private List<RecipeStage> recipeStages; // Stages of the recipe
    [SerializeField] private Transform spawnPoint; // Location where new items spawn
    [SerializeField] private List<ItemSpawnMapping> spawnMappings; // List of mappings for item spawning

    private Dictionary<string, GameObject> spawnMap = new Dictionary<string, GameObject>();
    private List<string> currentIngredients = new List<string>();
    private int currentStageIndex = 0;

    private void Start()
    {
        // Convert list to dictionary for quick lookup
        foreach (var mapping in spawnMappings)
        {
            spawnMap[mapping.ingredientName] = mapping.spawnPrefab;
        }
    }

    public void OnDrop(Draggable ingredient)
    {
        Destroy(ingredient.gameObject);

        // Add the dropped ingredient to the current list
        currentIngredients.Add(ingredient.name);

        // Spawn a new item if mapped
        if (spawnMap.ContainsKey(ingredient.name))
        {
            SpawnNewItem(spawnMap[ingredient.name]);
        }

        if (currentIngredients.Count == recipeStages[currentStageIndex].ingredients.Count)
        {
            CheckRecipeCorrectness();
        }
    }

    private void SpawnNewItem(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Force-enable Collider if it's disabled
        BoxCollider2D collider = newItem.GetComponent<BoxCollider2D>();
        if (collider != null) collider.enabled = true;

        // Force-enable the Draggable script if it's disabled
        Draggable draggable = newItem.GetComponent<Draggable>();
        if (draggable != null) draggable.enabled = true;
    }

    private void CheckRecipeCorrectness()
    {
        List<string> currentStage = recipeStages[currentStageIndex].ingredients;
        bool isCorrect = true;

        for (int i = 0; i < currentStage.Count; i++)
        {
            if (currentStage[i] != currentIngredients[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log($"Stage {currentStageIndex + 1} complete!");
            currentIngredients.Clear();
            currentStageIndex++;

            if (currentStageIndex >= recipeStages.Count)
            {
                Debug.Log("Recipe fully completed!");
                currentIngredients.Clear();
            }
        }
        else
        {
            Debug.Log($"Incorrect at Stage {currentStageIndex + 1}. Restarting...");
            Debug.Log($"Current ingredients: {string.Join(", ", currentIngredients)}");
            Debug.Log($"Expected ingredients: {string.Join(", ", currentStage)}");
            currentIngredients.Clear();
        }
    }
}
