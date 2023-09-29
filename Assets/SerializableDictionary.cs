using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        Debug.Log("Save " + keys.Count);

        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            EntityRelarionship[] ts = pair.Value as EntityRelarionship[];
            foreach (EntityRelarionship e in ts)
                Debug.Log(pair.Key + " : " + e.toEntity + " : " + e.type);

            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        Debug.Log("Load " + keys.Count);
        foreach (var Key in keys)
        {
            Debug.Log(Key);
        }

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
 }