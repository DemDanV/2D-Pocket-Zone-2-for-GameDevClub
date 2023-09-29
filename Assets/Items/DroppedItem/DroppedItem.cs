using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class DroppedItem : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] SpriteRenderer itemIcon;
    [SerializeField] Transform shadow;

    Vector3 initialPosition;

    [SerializeField] float animationSpeed = 100.0f; // Скорость анимации
    [SerializeField] float maxYOffset = 0.2f;     // Максимальное смещение по Y
    [SerializeField] float smoothTime = 0.2f;     // Время плавной остановки

    public InventoryItem Item => item;


    private Vector3 initialScale;

    public void Initialize(InventoryItem dropped)
    {
        item = dropped;
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (item != null)
            Initialize();
    }


    void Initialize()
    {
        initialScale = shadow.localScale;
        initialPosition = itemIcon.transform.position;
        itemIcon.sprite = item.Item.Icon;

        StartCoroutine(AnimateIdle());
    }


    IEnumerator AnimateIdle()
    {
        while (true)
        {
            float yPos = Mathf.Sin(Time.time * Mathf.Deg2Rad * animationSpeed);

            itemIcon.transform.position = initialPosition + Vector3.up * yPos * maxYOffset;
            shadow.localScale = initialScale - Vector3.one * yPos * maxYOffset;

            yield return null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == false)
            return;

        Inventory inventory = collision.transform.GetComponent<Inventory>();
        int retAmount = inventory.Add(item);
        if (retAmount > 0)
            item = new InventoryItem(item.Item, retAmount);
        else
            Destroy(gameObject);
    }

    public virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    // Метод для загрузки DroppedItem из сериализованных данных
    public void LoadFromSerializable(SerializableDroppedItem serializableItem)
    {
        item = new InventoryItem(serializableItem.inventoryItem);
        transform.position = serializableItem.position;
        gameObject.SetActive(true);
    }
}

[Serializable]
public class SerializableDroppedItem
{
    public SerializableInventoryItem inventoryItem; // Сериализованная информация об инвентарном предмете
    public Vector3 position;

    public SerializableDroppedItem (DroppedItem item)
    {
        inventoryItem = new SerializableInventoryItem(item.Item);
        position = item.transform.position;
    }
}
