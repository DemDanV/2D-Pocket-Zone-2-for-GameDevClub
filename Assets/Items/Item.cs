using System;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Item")]
public class Item : ScriptableObject, IComparable<Item>
{
    [SerializeField] protected GameObject prefab;
    public GameObject Prefab => prefab;

    [SerializeField] protected Sprite icon;
    public Sprite Icon => icon;

    public virtual bool Stackable { get { return true; } }


    public int CompareTo(Item other)
    {
        // A null value means that this object is greater.
        if (other == null)
            return 1;

        else
            return name.CompareTo(other.name);
    }

    public static Item LoadFromSerializable(SerializableItem from)
    {
        return (Item)Resources.Load("ScriptableObjects/" + from.name);
    }
}

[Serializable]
public class SerializableItem
{
    public string name;

    public SerializableItem(Item item)
    {
        name = item.name;
    }
}
