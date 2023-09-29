using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitiesRelationships", menuName = "Entity/Entities Relationships")]
public class EntitiesRelationships : ScriptableObject
{
    //[SerializeField] private List<EntityRelationships> entityRelationships;
    //public Dictionary<Type, EntityRelarionship[]> Relationships => entityRelationships;

    //private void Reset()
    //{
    //    Debug.Log("RESET");
    //    if (entityRelationships != null) return;

    //    var allEntityTypes = typeof(Entity).Assembly.GetTypes()
    //        .Where(t => t.IsSubclassOf(typeof(Entity)))
    //        .ToList();

    //    SetEntityRelationship(allEntityTypes);
    //}

    //public void SetEntityRelationship(List<Type> allEntityTypes)
    //{
    //    entityRelationships = new Dictionary<Type, EntityRelarionship[]>();
    //    for (int row = 0; row < allEntityTypes.Count; row++)
    //    {
    //        entityRelationships.Add(allEntityTypes[row], new EntityRelarionship[allEntityTypes.Count - row]);
    //        for (int column = 0; column < allEntityTypes.Count - row; column++)
    //        {
    //            EntityRelarionship toAdd = new EntityRelarionship
    //            {
    //                toEntity = allEntityTypes[column], // Здесь вам нужно установить соответствующий объект Entity
    //                type = EntityRelationshipType.Neutral // Или другой тип отношений по умолчанию
    //            };

    //            entityRelationships[allEntityTypes[row]][column] = toAdd;
    //        }
    //    }
    //    Debug.Log("Cause");
    //}

    //public void SetEntityRelationship(Dictionary<Type, EntityRelarionship[]> c)
    //{
    //    entityRelationships = c;
    //}
}