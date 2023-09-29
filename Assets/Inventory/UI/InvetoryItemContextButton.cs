using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvetoryItemContextButton : MonoBehaviour
{
    [SerializeField] TMP_Text tmp;
    [SerializeField] Button button;

    internal void Initialize(string v, Action value)
    {
        tmp.text = v;
        button.onClick.AddListener(() => value?.Invoke());
    }
}
