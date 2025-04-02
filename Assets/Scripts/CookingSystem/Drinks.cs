using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Drinks : MonoBehaviour
{
    private List<string> ingredientList = new List<string>();
    private string drinkIngredients;

    public Button restart;
    public Button done;
    

    private void Start()
    {
        restart.onClick.AddListener(ReloadScene);
        done.onClick.AddListener(AddInventory);
    }

    public void OnDrop(Draggable ingredient)
    {
        ingredientList.Add(ingredient.name);
        ingredientList.Sort();
        if(ingredientList.Contains("Milk") && ingredientList.Contains("Espresso"))
        {
            ingredientList.Remove("Milk");
            ingredientList.Remove("Espresso");
            ingredientList.Add("Latte");
          
        }
        if (ingredientList.Contains("Latte"))
        {
            ingredientList.Remove("Latte"); 
            ingredientList.Add("Latte"); 
        }
        drinkIngredients = string.Join(" ", ingredientList);
        Debug.Log(drinkIngredients);
        Destroy(ingredient.gameObject);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddInventory()
    {
        Item newDrink = ScriptableObject.CreateInstance<Item>();
        newDrink.itemName = drinkIngredients;
        newDrink.quantity = 1;
        InventorySystem.Instance.AddItem(newDrink);
        SceneManager.LoadScene("DogOnHighway");
    }
}
