using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Items/Weapon")]
public class WeaponItem : Item
{
    //[SerializeField] bool twoHanded;
    //public bool TwoHanded => twoHanded;

    public override bool Stackable => false;
}
