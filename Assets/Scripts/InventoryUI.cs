using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public Transform slotParent;
    private InventorySlotUI[] slotUIs;

    private void Start()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem tidak ditemukan untuk InventoryUI!");
            return;
        }
        if (slotParent == null)
        {
            Debug.LogError("slotParent belum di-assign di InventoryUI!");
            return;
        }

        // Inisialisasi slotUIs
        slotUIs = slotParent.GetComponentsInChildren<InventorySlotUI>();
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].Init(i, inventorySystem, this);
        }

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (slotUIs == null || inventorySystem.slots == null) return;

        // Jangan lewatkan slot yang out-of-range
        int count = Mathf.Min(slotUIs.Length, inventorySystem.slots.Length);
        for (int i = 0; i < count; i++)
        {
            slotUIs[i].SetSlot(inventorySystem.slots[i]);
        }
    }
}
