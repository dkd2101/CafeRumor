using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform itemButtonContainer;
    [SerializeField] private GameObject itemButtonPrefab;

    private List<Item> inventoryItems = new List<Item>();
    private Action<Item> onItemSelected;
    private bool isInventoryOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");

        if (inventoryPanel != null)
        {
            itemButtonContainer = inventoryPanel.transform.Find("ButtonContainer");

            if (itemButtonContainer == null)
            {
                UnityEngine.Debug.LogWarning("ButtonContainer not found under InventoryPanel.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("InventoryPanel not found in the scene.");
        }

        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory(null);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintInventory();
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem == null)
        {
            UnityEngine.Debug.LogWarning("Tried to add a null item to inventory.");
            return;
        }

        // Check if an item with the same name already exists
        var existingItem = inventoryItems.Find(i => string.Equals(i.itemName, newItem.itemName, StringComparison.OrdinalIgnoreCase));

        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity;
        }
        else
        {
            // If Item is a ScriptableObject, clone it to avoid shared reference conflicts
            Item clonedItem = new Item
            {
                itemName = newItem.itemName,
                description = newItem.description,
                quantity = newItem.quantity
            };

            inventoryItems.Add(clonedItem);
        }

        UnityEngine.Debug.Log($"Added Item: {newItem.itemName} x{newItem.quantity}");

        if (isInventoryOpen)
        {
            PopulateInventory();
        }
    }

    public bool HasItem(string itemName)
    {
        return inventoryItems.Exists(i => i.itemName.Equals(itemName, StringComparison.OrdinalIgnoreCase) && i.quantity > 0);
    }
    
    public bool HasItem(Item item)
    {
        return inventoryItems.Contains(item);
    }

    public void RemoveItem(string itemName)
    {
        var item = inventoryItems.Find(i => i.itemName.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            item.quantity--;
            if (item.quantity <= 0)
            {
                inventoryItems.Remove(item);
            }
        }

        if (isInventoryOpen)
        {
            PopulateInventory();
        }
    }

    public void OpenInventory(Action<Item> onItemSelectedCallback)
    {
        isInventoryOpen = true;
        inventoryPanel.SetActive(true);
        onItemSelected = onItemSelectedCallback;
        PopulateInventory();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryPanel.SetActive(false);
    }

    private void PopulateInventory()
    {
        foreach (Transform child in itemButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Check if the inventory has items
        if (inventoryItems.Count == 0)
        {
            UnityEngine.Debug.Log("Inventory is empty.");
            return;
        }

        // Iterate through every item in the inventory
        foreach (var item in inventoryItems)
        {
            if (item.quantity <= 0)
            {
                UnityEngine.Debug.Log($"Item '{item.itemName}' has zero quantity and won't be displayed.");
                continue;
            }

            // Instantiate the button and set its text
            var button = Instantiate(itemButtonPrefab, itemButtonContainer);
            var textComponent = button.GetComponentInChildren<TMP_Text>();

            if (textComponent != null)
            {
                textComponent.text = $"{item.itemName} (x{item.quantity})";
            }
            else
            {
                UnityEngine.Debug.LogWarning("TMP_Text component is missing in the item button prefab.");
            }

            // Ensure the button click registers correctly
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                onItemSelected?.Invoke(item);
                CloseInventory();
            });

            UnityEngine.Debug.Log($"Populated item: {item.itemName} x{item.quantity}");
        }
    }

    public void PrintInventory()
    {
        UnityEngine.Debug.Log("Current Inventory:");
        foreach (var item in inventoryItems)
        {
            UnityEngine.Debug.Log($"{item.itemName}: {item.quantity}");
        }
    }
}
