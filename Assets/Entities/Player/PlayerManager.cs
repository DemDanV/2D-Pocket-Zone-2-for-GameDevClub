using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Inventory inventory;
    PlayerEntity playerEntity;
    WeaponHandler weaponHandler;

    public Inventory Inventory => inventory;
    public PlayerEntity PlayerEntity => playerEntity;

    public WeaponHandler WeaponHandler => weaponHandler;

    [SerializeField] int inventoryCapacity = 10;

    //Weapon

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        playerEntity = GetComponent<PlayerEntity>();
        weaponHandler = GetComponentInChildren<WeaponHandler>();
        inventory.Initialize(inventoryCapacity);
    }

    public void LoadFromSerializable(SerializablePlayerInfo PSI)
    {
        playerEntity.LoadFromSerializable(PSI.entity);
        inventory.LoadFromSerializable(PSI.inventory);
        weaponHandler.LoadFromSerializable(PSI.weaponHadler);
    }
}
