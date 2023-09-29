using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Entity owner;
    [SerializeField] bool rotateTowardsTarget = true;

    WeaponItem equippedItemInfo;
    WeaponController equippedController;

    public WeaponItem EquippedItemInfo  => equippedItemInfo;
    public WeaponController WeaponController => equippedController;

    private void OnEnable()
    {
        InputManager.Controls.Player.Fire.performed += Fire_performed;
        InputManager.Controls.Player.Reload.performed += Reload_performed;
    }

    private void OnDisable()
    {
        InputManager.Controls.Player.Fire.performed -= Fire_performed;
        InputManager.Controls.Player.Reload.performed -= Reload_performed;
    }

    private void Fire_performed(InputAction.CallbackContext obj) { Shoot(); }
    private void Reload_performed(InputAction.CallbackContext context) { Reload(); }


    public void EquipWeapon(WeaponItem weaponItem)
    {
        if (weaponItem == null) return;

        equippedItemInfo = weaponItem;

        GameObject go = Instantiate(equippedItemInfo.Prefab, transform);
        if(go.TryGetComponent(out equippedController) == false)
        {
            Debug.Log("Failed attempt to equip the weapon. Make sure that WeaponItem have WeaponController on it.");
            Destroy(go);
            equippedItemInfo = null;
            return;
        }

        equippedController.SetOwner(owner);
    }

    private void Shoot()
    {
        if (equippedController == null)
        {
            Debug.Log("No weapon equipped");
            return;
        }
        equippedController.Shoot();
    }

    private void Reload()
    {
        equippedController.Reload();
    }

    public void LoadFromSerializable(SerializableWeaponHandler from)
    {
        EquipWeapon((WeaponItem)Item.LoadFromSerializable(from.equippedItemInfo));
        equippedController.LoadFromSerializable(from.equippedItemController);
    }
}


[Serializable]
public class SerializableWeaponHandler
{
    public SerializableItem equippedItemInfo;
    public SerializableWeaponController equippedItemController;

    public SerializableWeaponHandler(WeaponHandler wh)
    {
        equippedItemInfo = new SerializableItem(wh.EquippedItemInfo);
        equippedItemController = new SerializableWeaponController(wh.WeaponController);
    }
}