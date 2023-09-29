using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(EntitiesRelationships))]
public class EntitiesRelationshipsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //EntitiesRelationships entitiesRelationships = (EntitiesRelationships)target;

        //EditorGUILayout.LabelField("Entity Relationships:");

        //// Получаем список всех классов, унаследованных от Entity
        //var allEntityTypes = typeof(Entity).Assembly.GetTypes()
        //.Where(t => t.IsSubclassOf(typeof(Entity)))
        //.ToList();


        //EditorGUILayout.BeginHorizontal();

        //allEntityTypes.Reverse();
        //// Выводим заголовки столбцов
        //GUILayout.Space(EditorGUIUtility.labelWidth);
        //foreach (var entityType in allEntityTypes)
        //{
        //    GUILayout.Label(entityType.Name, GUILayout.Width(100));
        //}

        //EditorGUILayout.EndHorizontal();

        //Dictionary<Type, EntityRelarionship[]> entityRelationshipsCopy = new Dictionary<Type, EntityRelarionship[]>();

        //foreach (var row in entitiesRelationships.Relationships)
        //{
        //    EditorGUILayout.BeginHorizontal();

        //    GUILayout.Label(row.Key.Name, GUILayout.Width(EditorGUIUtility.labelWidth));

        //    // Создаем временную переменную для обратной последовательности
        //    for (int i = row.Value.Count() - 1; i >= 0; i--)
        //    {
        //        EntityRelationshipType RelationType = row.Value[i].type;

        //        EditorGUI.BeginChangeCheck();
        //        RelationType = (EntityRelationshipType)EditorGUILayout.EnumPopup(RelationType, GUILayout.Width(100));
        //        //if (EditorGUI.EndChangeCheck())
        //        //{
        //        //    // Обновляем значение отношения во временной переменной
        //        //    row.Value[i].type = RelationType;
        //        //    Debug.Log(row.Key.Name + " " + row.Value[i].toEntity.Name + " " + row.Value[i].type);
        //        //    EditorUtility.SetDirty(target);
        //        //}
        //    }

        //    entityRelationshipsCopy.Add(row.Key, row.Value);

        //    EditorGUILayout.EndHorizontal();
        //}

        //if (GUI.changed)
        //{
        //    entitiesRelationships.SetEntityRelationship(entityRelationshipsCopy);
        //}
    }
}