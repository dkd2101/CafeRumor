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
    public GameObject latte;
    public GameObject cup;
    
    private void Start()
    {
        restart.onClick.AddListener(ReloadScene);
        done.onClick.AddListener(AddInventory);
    }

    public void Update()
    {
        done.gameObject.SetActive(ingredientList.Count != 0);
    }

    public void OnDrop(Draggable ingredient)
    {
        ingredient.StartInBetweens();
        ingredientList.Add(ingredient.name);
        ingredientList.Sort();
        Destroy(ingredient.gameObject);
    }

    public void AddEspresso()
    {
        ingredientList.Add("Espresso Pod");
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AddInventory()
    {
        bool hasEspresso = ingredientList.Contains("Espresso Pod");
        bool hasMilk = ingredientList.Contains("Milk");

        if (hasEspresso && hasMilk)
        {
            ingredientList.Remove("Espresso Pod");
            ingredientList.Remove("Milk");
            ingredientList.Add("Latte");
        }
        else if (hasEspresso)
        {
            ingredientList.Remove("Espresso Pod");
            ingredientList.Add("Coffee");
        }
        
        drinkIngredients = string.Join(" ", ingredientList);
        Item newDrink = ScriptableObject.CreateInstance<Item>();
        newDrink.itemName = drinkIngredients;
        newDrink.quantity = 1;
        InventorySystem.Instance.AddItem(newDrink);
        cup.SetActive(false);
        latte.SetActive(true);
        Invoke("ShowWinPopup", 1f);
        restart.gameObject.SetActive(false);
        done.gameObject.SetActive(false);
    }

    private void ShowWinPopup()
    {
        FindObjectOfType<Popup>().ShowWinPopup($"You made a {drinkIngredients}!");
        
    }
}
