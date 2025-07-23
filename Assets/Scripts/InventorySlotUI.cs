using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;
    public Text nameText;
    public Button useButton;      // referensi tombol Use
    private int slotIndex;
    private InventorySystem invSys;
    private InventoryUI invUI;

    // dipanggil oleh InventoryUI saat setup
    public void Init(int index, InventorySystem sys, InventoryUI ui)
    {
        slotIndex = index;
        invSys = sys;
        invUI = ui;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(OnUseClicked);
    }

    public void SetSlot(InventorySlot slot)
    {
        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            icon.enabled = true;
            amountText.text = slot.amount > 0 ? slot.amount.ToString() : "";
            nameText.text = slot.item.itemName;
            useButton.interactable = slot.item.isConsumable;
        }
        else
        {
            icon.enabled = false;
            amountText.text = "";
            nameText.text = "";
            useButton.interactable = false;
        }
    }

    private void OnUseClicked()
    {
        if (invSys.UseItem(slotIndex))
        {
            invUI.RefreshUI();
        }
    }
}
