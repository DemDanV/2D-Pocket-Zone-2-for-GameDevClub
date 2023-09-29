using UnityEngine;

public class ItemDropManager : CircleSpawnArea
{
    public static ItemDropManager singleton;

    [SerializeField] DroppedItem droppedItemPrefab;

    private void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("There's more than one ItemDropManager");
        }
        singleton = this;
    }

    public DroppedItem Spawn(InventoryItem toDrop, Vector3 position)
    {
        DroppedItem DI = base.Spawn(droppedItemPrefab, position);

        DI.Initialize(toDrop);
        return DI;
    }

    public DroppedItem Spawn(InventoryItem toDrop, Vector3 center, float radius)
    {
        DroppedItem DI = base.Spawn(droppedItemPrefab, center, radius);

        DI.Initialize(toDrop);
        return DI;
    }

    public DroppedItem Spawn(InventoryItem toDrop, Vector3 center, float minRadius, float maxRadius)
    {
        DroppedItem DI = base.Spawn(droppedItemPrefab, center, minRadius, maxRadius);

        DI.Initialize(toDrop);
        return DI;
    }

    public DroppedItem Spawn(InventoryItem toDrop, Vector3 center, float radius, Vector3 direction)
    {
        DroppedItem DI = base.Spawn(droppedItemPrefab, center, radius, direction);

        DI.Initialize(toDrop);
        return DI;
    }
}