using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] EntityCharacteristic health;
    [SerializeField] InventoryItem dropItem;
    [SerializeField] Transform shootAt;
    [SerializeField] float seeDistance = 8f;
    [SerializeField] string entityName;

    public virtual string Name => entityName;
    public EntityCharacteristic Health => health;
    public InventoryItem DropItem => dropItem;
    public float SeeDistance => seeDistance;




    [SerializeField] EnemyLocator enemyLocator;
    public EnemyLocator EnemyLocator => enemyLocator;

    private void Start()
    {
        enemyLocator.SetSeeDistance(seeDistance);
    }

    public virtual Transform GetShootAtTransform()
    {
        return shootAt == null? transform : shootAt;
    }

    public virtual void TakeDamage(Vector3 direction, float damage)
    {
        Debug.Log("Took damage: " + direction + " : " + damage);
        health.Value -= damage;
        if (health.Value <= 0)
            Die();
    }

    protected virtual void Die()
    {
        ItemDropManager.singleton.Spawn(dropItem, transform.position);
        Destroy(gameObject);
    }

    public void LoadFromSerializable(SerializableEntity entity)
    {
        transform.transform.position = entity.transform.position;
        transform.transform.rotation = Quaternion.Euler(entity.transform.rotation);
        health.Clamp = entity.health.clamp;
        health.Value = entity.health.value;
        dropItem = new InventoryItem(entity.dropItem);
        seeDistance = entity.seeDistance;
        entityName = entity.entityName;
    }
}


[Serializable]
public class SerializableEntity
{
    public SerializableTransform transform;
    public SerializableEntityCharacteristic health;
    public SerializableInventoryItem dropItem;
    public float seeDistance;
    public string entityName;

    public SerializableEntity(Entity entity)
    {
        transform = new SerializableTransform(entity.transform);
        health = new SerializableEntityCharacteristic(entity.Health);
        dropItem = new SerializableInventoryItem(entity.DropItem);
        seeDistance = entity.SeeDistance;
        entityName = entity.Name;
    }
}

[Serializable]
public class SerializableTransform
{
    public Vector3 position;
    public Vector3 rotation;

    public SerializableTransform(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
    }
}