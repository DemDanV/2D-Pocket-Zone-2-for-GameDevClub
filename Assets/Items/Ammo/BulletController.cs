using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float bulletLifetime = 7;
    [SerializeField] float bulletDamage = 10;

    [SerializeField] GameObject bulletShellPrefab;

    [SerializeField] TrailRenderer trailRenderer;

    public virtual void Initialize(Entity target)
    {
        // Получите текущие точки следа
        Vector3[] trailPoints = new Vector3[2];

        // Обновите первую точку следа с текущей позицией объекта
        trailPoints[0] = transform.position;
        trailPoints[1] = target.GetShootAtTransform().position;

        // Установите обновленные точки следа обратно в TrailRenderer
        trailRenderer.SetPositions(trailPoints);

        Instantiate(bulletShellPrefab, transform.position, Quaternion.identity);
            Debug.Log("EEEEEEEEEE");
        target.TakeDamage(target.GetShootAtTransform().position - transform.position, bulletDamage);
        transform.position = target.GetShootAtTransform().position;

        if (trailRenderer == null)
            return;

        Destroy(gameObject, bulletLifetime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    HandleCollision(collision);
    //    Destroy(gameObject);
    //}

    //protected virtual void HandleCollision(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject.ToString() + " : " + collision.gameObject.layer);
    //    if (collision.collider == null) return;

    //    HitboxComponent hitboxC;
    //    collision.transform.TryGetComponent(out hitboxC);
    //    if (hitboxC == null) return;

    //    hitboxC.TakeDamage(rb.velocity, bulletDamage);
    //}
}
