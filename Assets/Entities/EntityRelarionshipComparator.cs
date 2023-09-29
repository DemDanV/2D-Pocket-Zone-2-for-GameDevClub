using System;
using System.Linq;
using UnityEngine;

public static class EntityRelarionshipComparator
{
    public static EntityRelationshipType CompareRelationship(Entity entity, Entity toEntity)
    {
        //EntityRelationships relarionships = EntityRelationshipManager.singleton.CurrentGlobalRelationships;

        //if (relarionships == null)
        //    return EntityRelationshipType.Neutral;

        //Type entityType = entity.GetType();
        //Type toEntityType = toEntity.GetType();

        //try
        //{
        //    var full = relarionships.Relationships.First(x => x.Key == entityType || x.Key == toEntityType);
        //    if (full.Key == entityType)
        //        return full.Value.First(x => x.toEntity == toEntityType).type;
        //    else
        //        return full.Value.First(x => x.toEntity == entityType).type;
        //}
        //catch (Exception)
        //{
        //    return EntityRelationshipType.Neutral;
        //}
        return EntityRelationshipType.Neutral;
    }
}

[Serializable]
public class EntityRelarionship
{
    [SerializeField] public Type toEntity;
    [SerializeField] public EntityRelationshipType type;
}

[Serializable]
public enum EntityRelationshipType
{
    Enemy = -1,
    Neutral = 0,
    Friend
}
