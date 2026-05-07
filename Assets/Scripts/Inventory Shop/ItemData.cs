using UnityEngine;

public enum ItemType
{
    Consumable,
    Charm,
    KeyItem,
    Currency
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

    public int maxStack = 99;
    public int buyPrice = 0;
    public int sellPrice = 0;

    public float healPercent = 0f;
    public float attackBoostPercent = 0f;
}