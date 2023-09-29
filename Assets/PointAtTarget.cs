using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtTarget : MonoBehaviour
{
    [SerializeField] EnemyLocator enemyLocator;
    [SerializeField] float radius = 2;
    [SerializeField] Transform IdlePosition;
    [SerializeField] float speed = 1;

    GameObject limbTargetRef;

    private void Start()
    {
        limbTargetRef = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        Vector3 targetPos;
        if (enemyLocator.Target != null)
        {
            Vector3 direction = enemyLocator.Target.position - transform.position;
            targetPos = transform.position + direction.normalized * radius;
            limbTargetRef.transform.position =
                    Vector3.Slerp(limbTargetRef.transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (enemyLocator.Target == null) return;

        Gizmos.color = Color.blue;
        Vector3 direction = enemyLocator.Target.position - transform.position;

        Gizmos.DrawLine(transform.position, transform.position + direction.normalized * radius);
    }
}
