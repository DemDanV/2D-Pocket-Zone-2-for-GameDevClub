using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class ItemCellDisplayer : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text amount;

    InventoryItem item;
    Button button;

    public ButtonClickedEvent onClick => button.onClick;

    public bool Empty => item == null;

    private void OnEnable()
    {
        ResetCell();
        button = GetComponent<Button>();
    }

    public void Display(InventoryItem toDisplay)
    {
        if (toDisplay == null)
        {
            ResetCell();
            return;
        }

        this.item = toDisplay;

        icon.sprite = item.Item.Icon;
        if(item.Item.Stackable)
            amount.text = item.Amount.ToString();
        else
            amount.text = "";

        icon.gameObject.SetActive(true);
        amount.gameObject.SetActive(true);
    }

    public void ResetCell()
    {
        item = null;
        icon.gameObject.SetActive(false);
        amount.gameObject.SetActive(false);
    }
}
