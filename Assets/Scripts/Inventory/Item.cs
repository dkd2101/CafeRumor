using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]

[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public int quantity;

    public override bool Equals(object other)
    {
        if (other is Item otherItem)
        {
            return itemName == otherItem.itemName && description == otherItem.description;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (itemName, description).GetHashCode();
    }
}
