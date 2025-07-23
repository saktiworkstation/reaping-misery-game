using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    [Header("Consumable Settings")]
    public bool isConsumable = false;
    public int healthRestore = 0;
    public int manaRestore = 0;
}
