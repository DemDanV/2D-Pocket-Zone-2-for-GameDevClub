using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventoryItem : IEquatable<InventoryItem> , IComparable<InventoryItem>
{
    [SerializeField] int amount;
    public int Amount => amount;
    [SerializeField] Item item;
    public Item Item => item;

    public UnityEvent<int> onAmountChanged;


    public InventoryItem(Item item, int amount)
    {
        this.item = item;

        if (item.Stackable == false && amount > 0)
            this.amount = 1;
        else
            this.amount = amount;

        onAmountChanged = new UnityEvent<int>();
    }

    public InventoryItem(SerializableInventoryItem from)
    {
        item = Item.LoadFromSerializable(from.item);
        amount = from.amount;

        onAmountChanged = new UnityEvent<int>();
    }

    public void Add(int  amount)
    {
        if (amount <= 0) return;

        this.amount += amount;
        onAmountChanged?.Invoke(this.amount);
    }

    public void Remove(int amount)
    {
        if (amount <= 0) return;

        this.amount -= amount;
        onAmountChanged?.Invoke(this.amount);
    }

    public bool Equals(InventoryItem other)
    {
        if (other == null) return false;
        return item.Equals(other.item) && amount.Equals(other.amount);
    }

    public int CompareTo(InventoryItem other)
    {
        // A null value means that this object is greater.
        if (other == null)
            return 1;

        int greater = item.CompareTo(other.item);
        if (greater < 0)
            return greater;
        else
            return -amount.CompareTo(other.amount);
    }
}

[Serializable]
public class SerializableInventoryItem
{
    public int amount;
    public SerializableItem item;

    public SerializableInventoryItem(InventoryItem inventoryItem)
    {
        amount = inventoryItem.Amount;
        item = new SerializableItem(inventoryItem.Item);
    }
}
