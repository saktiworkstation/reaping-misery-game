using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public ItemSO item;      // referensi ScriptableObject item
    public int amount = 1;   // jumlah per click

    private InventorySystem inventory;

    void Start()
    {
        // cari InventorySystem di scene
        inventory = FindFirstObjectByType<InventorySystem>();
    }

    // Unity built-in: dipanggil saat user klik collider ini
    void OnMouseDown()
    {
        if (inventory == null) return;

        // coba add ke inventory
        if (inventory.AddItem(item, amount))
        {
            // refresh UI
            FindFirstObjectByType<InventoryUI>().RefreshUI();
            // hapus dari dunia
            Destroy(gameObject);
        }
    }
}
