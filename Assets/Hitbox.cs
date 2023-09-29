using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] Entity owner;
    [SerializeField] HitboxComponent[] hitboxComponents;

    private void Start()
    {
        hitboxComponents = gameObject.GetComponentsInChildren<HitboxComponent>();
        foreach (HitboxComponent hitboxC in hitboxComponents)
        {
            hitboxC.SetManager(this);
        }
    }

    internal void TakeDamage(Vector2 velocity, float damage)
    {
        owner.TakeDamage(velocity, damage);
    }
}
