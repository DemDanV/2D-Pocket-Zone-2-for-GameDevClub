using UnityEngine;
using UnityEditor;
using System;

//[CustomPropertyDrawer(typeof(DictionaryOfTypeAndEntityRelationship))]
public class DictionaryOfTypeAndEntityRelationshipDrawer : PropertyDrawer
{
    //private const float lineHeight = 18f;
    //private const float spacing = 2f;

    //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //{
    //    int count = property.FindPropertyRelative("keys").arraySize;
    //    return (count + 1) * (lineHeight + spacing) + lineHeight; // +1 for the "Size" field
    //}

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    EditorGUI.BeginProperty(position, label, property);

    //    position.height = lineHeight;
    //    EditorGUI.LabelField(position, label);

    //    SerializedProperty keys = property.FindPropertyRelative("keys");
    //    SerializedProperty values = property.FindPropertyRelative("values");

    //    EditorGUI.BeginChangeCheck();

    //    // Draw the "Size" field
    //    position.y += lineHeight + spacing;
    //    EditorGUI.PropertyField(position, keys.FindPropertyRelative("Array.size"));

    //    // Draw key-value pairs
    //    for (int i = 0; i < keys.arraySize; i++)
    //    {
    //        position.y += lineHeight + spacing;
    //        EditorGUI.PropertyField(position, keys.GetArrayElementAtIndex(i), GUIContent.none);
    //        position.x += position.width + spacing;
    //        EditorGUI.PropertyField(position, values.GetArrayElementAtIndex(i), GUIContent.none);
    //        position.x -= position.width + spacing;
    //    }

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        property.serializedObject.ApplyModifiedProperties();
    //    }

    //    EditorGUI.EndProperty();
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Calculate the Rect for the label and dictionary field
        Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect dictionaryRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height - EditorGUIUtility.singleLineHeight);

        // Display the label
        EditorGUI.LabelField(labelRect, label);

        // Indent the content
        EditorGUI.indentLevel++;

        // Find the SerializedProperty for the keys and values
        SerializedProperty keys = property.FindPropertyRelative("keys");
        SerializedProperty values = property.FindPropertyRelative("values");

        // Calculate the total number of elements in the dictionary
        int dictionarySize = keys.arraySize;

        // Display the dictionary elements
        for (int i = 0; i < dictionarySize; i++)
        {
            Rect keyRect = new Rect(dictionaryRect.x, dictionaryRect.y + i * EditorGUIUtility.singleLineHeight, dictionaryRect.width / 2, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(dictionaryRect.x + dictionaryRect.width / 2, dictionaryRect.y + i * EditorGUIUtility.singleLineHeight, dictionaryRect.width / 2, EditorGUIUtility.singleLineHeight);

            // Display the key and value fields
            EditorGUI.PropertyField(keyRect, keys.GetArrayElementAtIndex(i), GUIContent.none);
            EditorGUI.PropertyField(valueRect, values.GetArrayElementAtIndex(i), GUIContent.none);
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty keysProperty = property.FindPropertyRelative("keys");
        SerializedProperty valuesProperty = property.FindPropertyRelative("values");

        if (keysProperty == null || valuesProperty == null)
        {
            return EditorGUIUtility.singleLineHeight; // Fallback height
        }

        // Calculate the total height of the dictionary elements
        int dictionarySize = keysProperty.arraySize;
        return EditorGUIUtility.singleLineHeight + dictionarySize * EditorGUIUtility.singleLineHeight;
    }
}
