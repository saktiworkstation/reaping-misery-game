using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class InventorySystem : MonoBehaviour
{
    [Header("Pengaturan Inventori")]
    public int size = 20;                  // Jumlah slot tetap
    [HideInInspector] public InventorySlot[] slots;

    // Database untuk semua item yang mungkin ada, diindeks berdasarkan nama item
    private Dictionary<string, ItemSO> itemDatabase;

    private void Awake()
    {
        // Inisialisasi array slot
        slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new InventorySlot(null, 0);
        }

        LoadItemDatabase();
    }

    private void LoadItemDatabase()
    {
        itemDatabase = new Dictionary<string, ItemSO>();
        ItemSO[] allItems = Resources.LoadAll<ItemSO>("Items"); // Memuat semua ItemSO dari folder Resources/Items

        foreach (ItemSO item in allItems)
        {
            if (!itemDatabase.ContainsKey(item.itemName))
            {
                itemDatabase.Add(item.itemName, item);
            }
            else
            {
                Debug.LogWarning($"ItemSO dengan nama duplikat ditemukan: {item.itemName}. Hanya satu yang akan dimuat.");
            }
        }
        Debug.Log($"Item database loaded with {itemDatabase.Count} items.");
    }

    /// <summary>
    /// Tambah item ke inventori. 
    /// Jika sudah ada di slot, jumlahnya di‐increment; 
    /// jika belum ada, masuk ke slot kosong.
    /// </summary>
    public bool AddItem(ItemSO newItem, int amount = 1)
    {
        // 1) Cek jika sudah ada, tambahkan jumlah
        for (int i = 0; i < size; i++)
        {
            if (slots[i].item == newItem)
            {
                slots[i].amount += amount;
                return true;
            }
        }
        // 2) Cari slot kosong
        for (int i = 0; i < size; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = newItem;
                slots[i].amount = amount;
                return true;
            }
        }
        Debug.LogWarning("Inventory penuh!");
        return false;
    }

    /// <summary>
    /// Hapus item dari inventori. Jika jumlah habis, slot dikosongkan.
    /// </summary>
    public bool RemoveItem(ItemSO itemToRemove, int amount = 1)
    {
        for (int i = 0; i < size; i++)
        {
            if (slots[i].item == itemToRemove)
            {
                slots[i].amount -= amount;
                if (slots[i].amount <= 0)
                {
                    slots[i].item = null;
                    slots[i].amount = 0;
                }
                return true;
            }
        }
        Debug.LogWarning("Item tidak ditemukan di inventori!");
        return false;
    }

    /// <summary>
    /// Pakai item di slot index.
    /// </summary>
    public bool UseItem(int slotIndex)
    {
        var slot = slots[slotIndex];
        if (slot.item != null && slot.item.isConsumable)
        {
            //// apply efek
            //if (slot.item.healthRestore != 0) stats.ModifyHealth(slot.item.healthRestore);
            //if (slot.item.manaRestore != 0) stats.ModifyMana(slot.item.manaRestore);

            GameManager.instance.player.Heal(slot.item.healthRestore);
            // konsumsi 1 unit
            RemoveItem(slot.item, 1);
            return true;
        }
        Debug.LogWarning("Item ini tidak bisa digunakan!");
        return false;
    }

    // --- FUNGSI BARU UNTUK SAVE/LOAD ---

    /// <summary>
    /// Mengubah data inventaris menjadi string untuk disimpan.
    /// Format: itemName1:amount1;itemName2:amount2;EMPTY_SLOT;itemName4:amount4...
    /// </summary>
    public string SerializeInventory()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                sb.Append(slots[i].item.itemName);
                sb.Append(":");
                sb.Append(slots[i].amount);
            }
            else
            {
                sb.Append("EMPTY_SLOT"); // Penanda untuk slot kosong
            }

            if (i < slots.Length - 1)
            {
                sb.Append(";"); // Pemisah antar slot
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Memuat data inventaris dari string yang disimpan.
    /// </summary>
    public void DeserializeInventory(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            Debug.LogWarning("Data inventaris untuk deserialisasi kosong.");
            // Pastikan slot direset jika data tidak ada atau invalid
            for (int i = 0; i < size; i++)
            {
                slots[i] = new InventorySlot(null, 0);
            }
            return;
        }

        string[] slotData = data.Split(';');
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < slotData.Length && !string.IsNullOrEmpty(slotData[i]))
            {
                if (slotData[i] == "EMPTY_SLOT")
                {
                    slots[i].item = null;
                    slots[i].amount = 0;
                }
                else
                {
                    string[] itemInfo = slotData[i].Split(':');
                    if (itemInfo.Length == 2)
                    {
                        string itemName = itemInfo[0];
                        if (itemDatabase.TryGetValue(itemName, out ItemSO itemSO))
                        {
                            slots[i].item = itemSO;
                            if (int.TryParse(itemInfo[1], out int amount))
                            {
                                slots[i].amount = amount;
                            }
                            else
                            {
                                Debug.LogWarning($"Gagal parse amount untuk item {itemName} di slot {i}. Data: {itemInfo[1]}");
                                slots[i].item = null; // Reset jika amount tidak valid
                                slots[i].amount = 0;
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"ItemSO dengan nama '{itemName}' tidak ditemukan di database. Slot {i} akan kosong.");
                            slots[i].item = null;
                            slots[i].amount = 0;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Format data item tidak valid di slot {i}. Data: {slotData[i]}");
                        slots[i].item = null;
                        slots[i].amount = 0;
                    }
                }
            }
            else
            {
                // Jika data slot tidak ada (misalnya save lama atau data korup), set slot menjadi kosong
                slots[i].item = null;
                slots[i].amount = 0;
            }
        }
        // Jika ada UI Inventaris, panggil refresh di sini
        // Misalnya: InventoryUI.instance.RefreshUI();
    }
}
