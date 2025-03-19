using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;

    void Start()
    {
        Item burger = ScriptableObject.CreateInstance<Item>();
        burger.itemName = "Burger";
        burger.description = "A tasty burger";
        burger.quantity = 1;

        inventorySystem.AddItem(burger);
    }
}
