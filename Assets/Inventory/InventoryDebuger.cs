using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryDebuger : MonoBehaviour
{
    [SerializeField] List<InventoryItem> addItems;

    [SerializeField] InventoryItem addEachFire;

    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        foreach (InventoryItem item in addItems)
        {
            inventory.Add(item);
        }
    }

    private void OnEnable()
    {
        InputManager.Controls.Player.Fire.performed += Fire_performed;
    }

    private void OnDisable()
    {
        InputManager.Controls.Player.Fire.performed -= Fire_performed;
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        inventory.Add(addEachFire);
    }
}
