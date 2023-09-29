using System;
using UnityEngine;

public class InvetoryItemContextMenu : MonoBehaviour
{
    [SerializeField] InvetoryItemContextButton contextButton;
    [SerializeField] Transform content;
    internal void AddButton(string v, Action value)
    {
        InvetoryItemContextButton inst = Instantiate(contextButton, content);
        inst.Initialize(v, value);
    }

    public void Clean()
    {
        foreach(Transform t in content)
            Destroy(t.gameObject);
    }
}
