using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemText; 
    private bool isInventoryOpen = false;

    private List<string> inventoryItems = new List<string>();

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        UpdateInventoryText();
    }

    public void AddItem(string itemName)
    {
        inventoryItems.Add(itemName);
        if (isInventoryOpen) UpdateInventoryText();
        Debug.Log($"Item added: {itemName}");
    }

    public void RemoveItem(string itemName)
    {
        inventoryItems.Remove(itemName);
        if (isInventoryOpen) UpdateInventoryText();
    }

    private void UpdateInventoryText()
    {
        if (inventoryItems.Count == 0)
        {
            itemText.text = "Inventory:";
        }
        else
        {
            itemText.text = "Inventory:\n" + string.Join("\n", inventoryItems);
        }
    }
}