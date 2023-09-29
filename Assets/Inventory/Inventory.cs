using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class Inventory : MonoBehaviour
{
    protected List<InventoryItem> items;
    public IReadOnlyList<InventoryItem> Items => items;

    protected int cellStackMaxSize = 99;

    public int Capacity => items.Capacity;

    public bool CheckFreeCell
    {
        get
        {
            return items.Count < items.Capacity;
        }
    }

    public void Initialize(int capacity)
    {
        items = new List<InventoryItem>(capacity);
    }

    public virtual int Add(InventoryItem item)
    {
        return Add(item.Item, item.Amount);
    }

    public virtual int Add(Item item, int amount)
    {
        if (amount <= 0)
            return 0;

        // Если предмет не стакается (не суммируется в одной ячейке)
        if (!item.Stackable)
        {
            if (CheckFreeCell)
            {
                // Добавляем один предмет
                amount -= 1;
                items.Add(new InventoryItem(item, 1));
                items.Sort();

                //// Если добавленный предмет является оружием и в руке у игрока нет оружия, экипируем его
                //if (item is WeaponItem && player.currentWeapon == null)
                //{
                //    player.EquipWeapon((WeaponItem)item);
                //}
            }
            else
                return amount;
        }
        else
        {
            // Проверяем, можем ли мы добавить предметы в существующие стаки
            bool canAddAmount = items.Any(x => x.Item == item && x.Amount < cellStackMaxSize);

            if (!canAddAmount)
            {
                if (!CheckFreeCell)
                    return amount;

                // Пытаемся добавить максимальное количество предметов в новую ячейку
                int toAdd = Mathf.Min(amount, cellStackMaxSize);
                items.Add(new InventoryItem(item, toAdd));
                items.Sort();
                amount -= toAdd;
            }
            else
            {
                // Находим первый подходящий стак для добавления
                InventoryItem invItem = items.First(x => x.Item == item && x.Amount < cellStackMaxSize);

                // Сколько предметов можно добавить в этот стак
                int spaceAvailable = cellStackMaxSize - invItem.Amount;

                // Пытаемся добавить столько, сколько есть места
                int toAdd = Mathf.Min(amount, spaceAvailable);
                invItem.Add(toAdd);
                items.Sort();
                amount -= toAdd;
            }
        }

        return Add(item, amount);
    }

    public virtual int Remove(InventoryItem item)
    {
        return Remove(item.Item, item.Amount);
    }

    public virtual int Remove(Item item, int amount)
    {
        if(amount <= 0) return amount;

        if (item == null) return amount;

        if (items.Any(x => x.Item == item) == false) return amount;

        while (amount > 0)
        {
            if (items.Any(x => x.Item == item) == false) return amount;

            InventoryItem removeFrom = items.Last(x => x.Item == item);
            if (removeFrom == null)
                return amount;

            //amount -= removeFrom.Amount;
            if(removeFrom.Amount - amount > 0)
            {
                removeFrom.Remove(amount);
                amount = 0;
            }
            else
            {
                amount -= removeFrom.Amount;
                items.Remove(removeFrom);
            }
        }

        return 0;
    }

    public virtual void RemoveAt(int index)
    {
        if(index < 0 || index >= items.Count) return;

        items.RemoveAt(index);
    }

    public virtual InventoryItem Pull(Item item)
    {
        if (item == null) return null;

        if (items.Any(x => x.Item == item) == false) return null;

        InventoryItem pull = items.Last(x => x.Item == item);

        items.Remove(pull);

        return pull;
    }

    public virtual int CheckAmount(Item item, int amount)
    {
        if (item == null) return amount;

        if (items.Any(x => x.Item == item) == false) return amount;

        List<InventoryItem> itemsCopy = items.FindAll(x => x.Item == item);

        while (amount > 0)
        {
            InventoryItem removeFrom = itemsCopy.Last(x => x.Item == item);
            if (removeFrom == null)
                return amount;

            amount -= removeFrom.Amount;
            if (amount < 0)
                removeFrom.Add(-amount);
            else
                itemsCopy.Remove(removeFrom);
        }

        return 0;
    }

    public virtual bool Contains(Item item)
    {
        return items.Any(x => x.Item == item);
    }

    public void LoadFromSerializable(SerializableInventory from)
    {
        items = new List<InventoryItem>(from.capacity);
        for (int i = 0; i < from.items.Length; i++)
        {
            items.Add(new InventoryItem(from.items[i]));
        }
    }
}

[Serializable]
public class SerializableInventory
{
    public SerializableInventoryItem[] items;
    public int capacity;

    public SerializableInventory(Inventory inventory)
    {
        capacity = inventory.Capacity;

        items = new SerializableInventoryItem[inventory.Items.Count];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SerializableInventoryItem(inventory.Items[i]);
        }
    }
}
