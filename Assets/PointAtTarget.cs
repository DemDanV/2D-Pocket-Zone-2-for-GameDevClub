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
    Vector3 startLocalPos;

    private void Start()
    {
        limbTargetRef = transform.GetChild(0).gameObject;
        startLocalPos = limbTargetRef.transform.localPosition;
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
        else
        {
            limbTargetRef.transform.localPosition =
                    Vector3.Slerp(limbTargetRef.transform.localPosition, startLocalPos, speed * Time.deltaTime);
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
