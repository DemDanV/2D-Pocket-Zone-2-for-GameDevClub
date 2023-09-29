using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyLocator : MonoBehaviour
{
    [SerializeField] Entity owner;
    [SerializeField] List<Entity> enemies;

    [Description("If the distance between the nearest target and the current target is greater than this value, the current target is changed to the nearest target")]
    [SerializeField] float switchDistance = 2f;

    float seeDistance;
    CircleCollider2D circleCollider2D;


    Entity nearestTarget;
    Entity target;
    public Transform Target => target?.GetShootAtTransform();
    public Entity TargetEntity => target;


    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        SetSeeDistance(seeDistance);
    }

    public void SetSeeDistance(float radius)
    {
        seeDistance = radius;
        circleCollider2D.radius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Entity toEntity = other.GetComponent<Entity>();
        if(enemies.Any(x => x.GetType().IsAssignableFrom(toEntity.GetType())))
        {
            if (nearestTarget == null || IsCloser(toEntity, nearestTarget))
            {
                nearestTarget = toEntity;
                if (target == null) target = toEntity;
            }
        }

        if (target != nearestTarget && IsSwitchDistanceExceeded())
        {
            target = nearestTarget;
        }
    }

    private bool IsCloser(Entity entity1, Entity entity2)
    {
        float distance1 = Vector3.Distance(transform.position, entity1.transform.position);
        float distance2 = Vector3.Distance(transform.position, entity2.transform.position);
        return distance1 < distance2;
    }

    private bool IsSwitchDistanceExceeded()
    {
        if (target == null && nearestTarget != null) return true;

        if (target == null || nearestTarget == null) return false;

        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        float nearestDistance = Vector3.Distance(transform.position, nearestTarget.transform.position);

        return targetDistance - nearestDistance >= switchDistance;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target?.transform)
            target = null;
        else if(collision.transform == target?.transform)
            nearestTarget = null;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawLine(transform.position, Target.position);
    }
}
