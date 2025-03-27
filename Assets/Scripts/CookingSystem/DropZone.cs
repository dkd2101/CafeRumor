using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DropZone : MonoBehaviour
{
    [SerializeField] private List<RecipeStage> recipeStages; // Stages of the recipe
    [SerializeField] private Transform spawnPoint; // Location where new items spawn
    [SerializeField] private List<ItemSpawnMapping> spawnMappings; // List of mappings for item spawning

    private Dictionary<string, GameObject> spawnMap = new Dictionary<string, GameObject>();
    private List<string> currentIngredients = new List<string>();
    private int currentStageIndex = 0;
    public GameObject recipe;
    public List<GameObject> recipes = new List<GameObject>();
    private Tooltip tooltip;
    private Popup popup;
    private string spawnIngredientName;
    private void Start()
    {
        // Convert list to dictionary for quick lookup
        foreach (var mapping in spawnMappings)
        {
            spawnMap[mapping.ingredientName] = mapping.spawnPrefab;
        }

        tooltip = FindObjectOfType<Tooltip>();
        popup = FindObjectOfType<Popup>();
    }

    
    public void OnDrop(Draggable ingredient)
    {
        Destroy(ingredient.gameObject);

        if (currentStageIndex >= recipeStages.Count)
        {
            popup.ShowErrorPopup();
            return;
        }

        // Add the dropped ingredient to the current list
        currentIngredients.Add(ingredient.name);

        if (currentIngredients.Count == recipeStages[currentStageIndex].ingredients.Count)
        {
            CheckRecipeCorrectness();
        }
    }

    
    
    private void SpawnNewItem(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        newItem.name = prefab.name;
        tooltip.ShowTooltip(prefab.name);
        spawnIngredientName = prefab.name;

        // Force-enable Collider if it's disabled
        BoxCollider2D collider = newItem.GetComponent<BoxCollider2D>();
        if (collider != null) collider.enabled = true;

        // Force-enable the Draggable script if it's disabled
        Draggable draggable = newItem.GetComponent<Draggable>();
        if (draggable != null) draggable.enabled = true;
        
        // this one works
         foreach (var entry in spawnMap)
         {
             if (entry.Value.name == recipe.name)
             {
                 popup.ShowWinPopup($"You successfully made the {spawnIngredientName} recipe!");
             }
         }
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

            if (i >= currentIngredients.Count)
            {
                isCorrect = false;
                popup.ShowErrorPopup();
                break;
            }
        }

        if (isCorrect)
        {
            string lastIngredient = currentIngredients[currentIngredients.Count - 1];
            if (spawnMap.ContainsKey(lastIngredient))
            {
                SpawnNewItem(spawnMap[lastIngredient]);
            }

            currentIngredients.Clear();
            currentStageIndex++;

        }
        else
        {
            Debug.Log($"Current ingredients: {string.Join(", ", currentIngredients)}");
            Debug.Log($"Expected ingredients: {string.Join(", ", currentStage)}");
            popup.ShowErrorPopup();
        }
    }
}
