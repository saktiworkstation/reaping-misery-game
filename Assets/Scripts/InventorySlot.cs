using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemSO item;
    public int amount;

    public InventorySlot(ItemSO item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
