using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityRelationships", menuName = "Entity/Entity Relationships")]
public class EntityRelationships : ScriptableObject
{
    //[SerializeField] List<EntityRelarionships> entityRelationships;
    //public IReadOnlyList<EntityRelarionships> Relationships => entityRelationships;

    //private void Reset()
    //{
    //    Debug.Log("RESET");
    //    //if (entityRelationships != null) return;

    //    var allEntityTypes = typeof(Entity).Assembly.GetTypes()
    //        .Where(t => t.IsSubclassOf(typeof(Entity)))
    //        .ToList();

    //    SetEntityRelationship(allEntityTypes);
    //}

    //public void SetEntityRelationship(List<Type> allEntityTypes)
    //{
    //    entityRelationships = new List<EntityRelarionships>();

    //    //List<EntityRelarionship> empty = new List<EntityRelarionship>();

    //    for (int i = 0; i < allEntityTypes.Count; i++)
    //    {
    //        EntityRelarionships s = new EntityRelarionships(allEntityTypes[j]);

    //        for (int j = 0; j < allEntityTypes.Count; j++)
    //        {
    //        }

    //            //entityRelationships.Add(allEntityTypes[row], new EntityRelarionship[allEntityTypes.Count]);
    //            //for (int column = 0; column < allEntityTypes.Count; column++)
    //            //{
    //            //    EntityRelarionship toAdd = new EntityRelarionship
    //            //    {
    //            //        toEntity = allEntityTypes[column], // Здесь вам нужно установить соответствующий объект Entity
    //            //        type = EntityRelationshipType.Neutral // Или другой тип отношений по умолчанию
    //            //    };

    //        //    entityRelationships[allEntityTypes[row]][column] = toAdd;
    //        //}
    //        }
    //    Debug.Log("Cause");
    //}

    ////public void SetEntityRelationship(List<Type> allEntityTypes)
    ////{
    ////    entityRelationships = new DictionaryOfTypeAndEntityRelationship();
    ////    for (int row = 0; row < allEntityTypes.Count; row++)
    ////    {
    ////        entityRelationships.Add(allEntityTypes[row], new EntityRelarionship[allEntityTypes.Count - row]);
    ////        for (int column = 0; column < allEntityTypes.Count - row; column++)
    ////        {
    ////            EntityRelarionship toAdd = new EntityRelarionship
    ////            {
    ////                toEntity = allEntityTypes[column], // Здесь вам нужно установить соответствующий объект Entity
    ////                type = EntityRelationshipType.Neutral // Или другой тип отношений по умолчанию
    ////            };

    ////            entityRelationships[allEntityTypes[row]][column] = toAdd;
    ////        }
    ////    }
    ////    Debug.Log("Cause");
    ////}

    //public void SetEntityRelationship(DictionaryOfTypeAndEntityRelationship c)
    //{
    //    entityRelationships = c;
    //}
}

[Serializable]
public class EntityRelarionships
{
    [SerializeField] Type entity;
    public Type Entity => entity;
    [SerializeField] List<EntityRelarionship> relations;
    public IReadOnlyList<EntityRelarionship> Relationship => relations;


    public EntityRelarionships(Type entity)
    {
        this.entity = entity;
        relations = new List<EntityRelarionship>();
    }
}
