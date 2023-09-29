using UnityEngine;

public class WeaponHandlerDebugger : MonoBehaviour
{
    [SerializeField] WeaponItem toAdd;

    void Start()
    {
        WeaponHandler weaponHandler = GetComponent<WeaponHandler>();
        weaponHandler.EquipWeapon(toAdd);
    }
}
