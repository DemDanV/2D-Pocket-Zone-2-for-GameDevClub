using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] Item ammo;
    [SerializeField] Inventory ammoInventory;
    [SerializeField] int magSize = 30;

    InventoryItem slot;
    public InventoryItem Slot => slot;
    public int MagSize => magSize;

    public Item Ammo => ammo;
    public string CanReload
    {
        get
        {
            if (ammoInventory == null || (ammoInventory.Contains(Ammo) == false))
                return "No more ammo";

            if(magSize - slot.Amount <= 0)
                return "Magazine is full";

            return "";
        }
    }

    public bool NeedReload => slot.Amount <= 0;

    public bool GetAmmo()
    {
        if(slot.Amount <= 0) return false;
        slot.Remove(1);
        return true;
    }

    public bool Reload()
    {
        int ammoNeed = magSize - slot.Amount;

        if (ammoNeed <= 0) return true;

        int returned = ammoInventory.Remove(ammo, ammoNeed);

        slot.Add(ammoNeed - returned);

        return true;
    }

    internal void UseInventory(Inventory inventory)
    {
        ammoInventory = inventory;
        slot = new InventoryItem(Ammo, 0);
        Debug.LogWarning("DEBUG DELETE THE FOLLOWING LINE (if you are not debuggin this feature :3)");
        //slot.Add(30);
    }

    public void LoadFromSerializable(SerializableMagazine from)
    {
        ammo = Item.LoadFromSerializable(from.ammo);
        magSize = from.magSize;
        slot = new InventoryItem(from.slot);
    }
}

[Serializable]
public class SerializableMagazine
{
    public SerializableItem ammo;
    public int magSize;
    public SerializableInventoryItem slot;

    public SerializableMagazine(Magazine magazine)
    {
        ammo = new SerializableItem(magazine.Ammo);
        magSize = magazine.MagSize;
        slot = new SerializableInventoryItem(magazine.Slot);
    }
}
