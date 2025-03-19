using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]

[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public int quantity;
}
