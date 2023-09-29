using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoInfoDisplayer : MonoBehaviour
{
    [SerializeField] Image ammoIcon;
    [SerializeField] TMP_Text MagCapacity;
    [SerializeField] TMP_Text MagLeft;
    [SerializeField] Magazine toDisplay;

    private void Start()
    {
        InventoryItem inventoryItem = toDisplay.Slot;
        ammoIcon.sprite = inventoryItem.Item.Icon;
        MagCapacity.text = toDisplay.MagSize.ToString();
        MagLeft.text = inventoryItem.Amount.ToString();
        toDisplay.Slot.onAmountChanged.AddListener(RefreshMagLeft);
    }

    public void RefreshMagLeft(int newValue)
    {
        MagLeft.text = newValue.ToString();
    }
}
