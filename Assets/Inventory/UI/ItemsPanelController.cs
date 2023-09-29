using System.Collections.Generic;
using UnityEngine;

public class ItemsPanelController : MonoBehaviour
{
    Inventory inventoryToDisplay;
    [SerializeField] private ItemCellDisplayer itemCellDisplayerPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private InvetoryItemContextMenu itemContextMenu;

    private List<ItemCellDisplayer> displayers = new List<ItemCellDisplayer>();

    private void OnEnable()
    {
        if (inventoryToDisplay == null)
            inventoryToDisplay = FindObjectOfType<PlayerManager>().Inventory;

        if (inventoryToDisplay == null)
            return;

        ClearContent();
        displayers.Clear();

        for (int i = 0; i < inventoryToDisplay.Capacity; i++)
        {
            ItemCellDisplayer displayer = Instantiate(itemCellDisplayerPrefab, content);
            displayers.Add(displayer);
            displayer.onClick.AddListener(() => DisplayContextMenu(displayer));
        }

        RefreshDisplayers();
    }

    private void DisplayContextMenu(ItemCellDisplayer forDisplayer)
    {
        itemContextMenu.Clean();

        if (forDisplayer.Empty) return;

        itemContextMenu.transform.position = forDisplayer.transform.position;
        itemContextMenu.AddButton("drop", () => DropItem(forDisplayer));
    }

    private void DropItem(ItemCellDisplayer forDisplayer)
    {
        itemContextMenu.Clean();

        int index = displayers.FindIndex(x => x == forDisplayer);

        InventoryItem item = inventoryToDisplay.Items[index];
        inventoryToDisplay.RemoveAt(index);
        forDisplayer.ResetCell();

        RefreshDisplayers();

        ItemDropManager.singleton.Spawn(item, inventoryToDisplay.gameObject.transform.position, 2, inventoryToDisplay.gameObject.transform.right);
    }

    private void ClearContent()
    {
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }
    }

    private void RefreshDisplayers()
    {
        var items = inventoryToDisplay.Items;

        for (int i = 0; i < items.Count; i++)
        {
            displayers[i].Display(items[i]);
        }

        for (int i = items.Count; i < displayers.Count; i++)
        {
            displayers[i].ResetCell();
        }
    }

    private void OnDisable()
    {
        ClearContent();
    }
}