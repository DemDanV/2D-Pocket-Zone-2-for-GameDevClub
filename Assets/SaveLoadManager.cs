using System;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // Ссылка на DroppedItem
    [SerializeField] private DroppedItem droppedItemPrefab;
    [SerializeField] private PlayerManager gamePlayerPrefab;
    [SerializeField] private Flesh fleshPrefab;

    public string Save()
    {
        DroppedItem[] droppedItems = FindObjectsOfType<DroppedItem>();
        SerializableDroppedItems SDIS = new SerializableDroppedItems(droppedItems);

        PlayerManager playerM = FindObjectOfType<PlayerManager>();
        SerializablePlayerInfo SPI = new SerializablePlayerInfo(playerM);

        Flesh[] fleshes = FindObjectsOfType<Flesh>();
        SerializableEntities SES = new SerializableEntities(fleshes);


        SerializableSaveLoadManager SSLM = new SerializableSaveLoadManager();
        SSLM.SDIS = SDIS;
        SSLM.SPI = SPI;
        SSLM.SES = SES;

        string ret = JsonUtility.ToJson(SSLM);

        Debug.Log(ret);
        return ret;
    }


    // Метод для загрузки DroppedItems из файла
    public void Load(string json)
    {
        if (string.IsNullOrEmpty(json))
            return; // Возвращаем, если данные не были найдены

        BulletController[] bullets = FindObjectsOfType<BulletController>();
        foreach (BulletController b in bullets)
        {
            Destroy(b.gameObject);
        }

        DroppedItem[] droppedItems = FindObjectsOfType<DroppedItem>();
        foreach (DroppedItem item in droppedItems)
        {
            Destroy(item.gameObject);
        }

        PlayerEntity player = FindObjectOfType<PlayerEntity>();
        if(player != null)
            Destroy(player.gameObject);

        Flesh[] fleshes = FindObjectsOfType<Flesh>();
        foreach (Flesh f in fleshes)
        {
            Destroy(f.gameObject);
        }

        SerializableSaveLoadManager SSLM = JsonUtility.FromJson<SerializableSaveLoadManager>(json);
        SerializableDroppedItems SDI = SSLM.SDIS;
        SerializableDroppedItem[] SDIArray = SDI.items;
        foreach (SerializableDroppedItem item in SDIArray)
        {
            DroppedItem droppedItem = Instantiate(droppedItemPrefab);
            droppedItem.LoadFromSerializable(item);
        }

        SerializablePlayerInfo SPI = SSLM.SPI;
        PlayerManager playerGO = Instantiate(gamePlayerPrefab);
        playerGO.LoadFromSerializable(SPI);

        SerializableEntities SES = SSLM.SES;
        SerializableEntity[] SESArray = SES.entities;
        foreach (SerializableEntity fleshEntity in SESArray)
        {
            Flesh flesh = Instantiate(fleshPrefab);
            flesh.LoadFromSerializable(fleshEntity);
        }
    }
}

[Serializable]
class SerializableSaveLoadManager
{
    public SerializableDroppedItems SDIS;
    public SerializablePlayerInfo SPI;
    public SerializableEntities SES;
}

[Serializable]
class SerializableDroppedItems
{
    public SerializableDroppedItem[] items;

    public SerializableDroppedItems(DroppedItem[] droppedItems)
    {
        items = new SerializableDroppedItem[droppedItems.Length];
        for (int i = 0; i < droppedItems.Length; i++)
        {
            items[i] = new SerializableDroppedItem(droppedItems[i]);
        }
    }
}

[Serializable]
class SerializableEntities
{
    public SerializableEntity[] entities;

    public SerializableEntities(Entity[] entities)
    {
        this.entities = new SerializableEntity[entities.Length];
        for (int i = 0; i < entities.Length; i++)
        {
            this.entities[i] = new SerializableEntity(entities[i]);
        }
    }
}

[Serializable]
public class SerializablePlayerInfo
{
    public SerializableEntity entity;
    public SerializableInventory inventory;
    public SerializableWeaponHandler weaponHadler;

    public SerializablePlayerInfo(PlayerManager playerM)
    {
        entity = new SerializableEntity(playerM.PlayerEntity);
        inventory = new SerializableInventory(playerM.Inventory);
        weaponHadler = new SerializableWeaponHandler(playerM.WeaponHandler);
    }
}