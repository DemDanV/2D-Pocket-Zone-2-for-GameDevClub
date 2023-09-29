using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] float damageMultiplayer = 1f;

    Hitbox manager;

    internal void SetManager(Hitbox hitbox)
    {
        manager = hitbox;
    }

    internal void TakeDamage(Vector2 velocity, float bulletDamage)
    {
        manager.TakeDamage(velocity, bulletDamage * damageMultiplayer);
    }
}
