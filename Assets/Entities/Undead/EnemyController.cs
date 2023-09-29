using Pathfinding;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] EnemyLocator enemyLocator;
    public enum EnemyState
    {
        Idle,
        Wander,
        Seek,
        Chase,
        Attack
    }


    public float wanderRadius = 5f;
    public float chaseRange = 8f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    [Description("How close to player to start attacking. AttackRange will be mutlt by this value.")]
    public float multiplayerAttackRange = 0.5f;
    public float damage = 34;

    public float chaseBuildPathCooldown = 0.5f;


    [Header("Physics")]
    public float speed = 3f;
    public float nextWaypointDistance = 3f;

    [Header("Custom Behavior")]
    public bool directionLookEnabled = true;
    [SerializeField] Transform visuals;
    EnemyLocator enemyLocator;

    [SerializeField]
    EnemyState currentState = EnemyState.Idle;
    private Path path;
    private int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;


    private bool canAttack = true;
    private Coroutine nextStateTimer;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyLocator = GetComponent<Entity>().EnemyLocator;

        enemyLocator.SetSeeDistance(chaseRange);

        // Запускаем корутину для обновления состояния.
        StartCoroutine(UpdateState());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator UpdateState()
    {
        while (true)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    IdleState();
                    break;
                case EnemyState.Wander:
                    WanderState();
                    break;
                case EnemyState.Seek:
                    SeekState();
                    break;
                case EnemyState.Chase:
                    ChaseState();
                    break;
                case EnemyState.Attack:
                    AttackState();
                    break;
            }

            yield return null; // Ждем один кадр перед обновлением состояния.
        }
    }

    void IdleState()
    {
        // Реализуйте логику состояния "Idle" (пример: ожидание, патрулирование и т. д.).
        animator.SetTrigger("Idle");

        // Можно переключиться в другое состояние при выполнении определенного условия.
        if (IsTargetVisible())
        {
            if (IsTargetInAttackRange())
                currentState = EnemyState.Attack;
            else
                currentState = EnemyState.Chase;

            if (nextStateTimer != null)
            {
                StopCoroutine(nextStateTimer);
                nextStateTimer = null;
            }
        }
        else
        {
            if (path == null && nextStateTimer == null)
            {
                float time = Random.value * 10;
                nextStateTimer = StartCoroutine(SwitchToStateTimer(time, EnemyState.Wander));
            }
        }
    }

    void WanderState()
    {
        // Реализуйте логику состояния "Wander" (патрулирование, блуждание и т. д.).
        if (path != null)
        {
            PathFollow();
        }
        else if (path == null && !IsTargetVisible() && seeker.IsDone())
        {
            Vector3 targetPos = GetRandomPosition(wanderRadius);
            seeker.StartPath(rb.position, targetPos, OnPathComplete);
        }

        // Можно переключиться в другое состояние при выполнении определенного условия.
        if (IsTargetVisible())
        {
            ForgetPath();

            if (IsTargetInAttackRange())
                currentState = EnemyState.Attack;
            else
                currentState = EnemyState.Chase;
        }
        else
        {
            if (path == null && seeker.IsDone())
            {
                currentState = EnemyState.Idle;
            }
        }
    }

    public Vector3 GetRandomPosition(float maxDistance)
    {
        Vector2 rand2D = Random.insideUnitCircle * maxDistance;
        Vector3 fromPos = transform.position + (Vector3)rand2D;

        return fromPos;
    }

    bool canSeek = true;

    void SeekState()
    {
        // Реализуйте логику состояния "Seek" (поиск игрока, когда игрок находится в области видимости).
        if (path != null)
        {
            PathFollow();
        }
        else if(canSeek)
        {
            canSeek = false;
            animator.SetTrigger("Seek");
        }

        // Можно переключиться в другое состояние при выполнении определенного условия.

        if (IsTargetVisible())
        {
            if (IsTargetInAttackRange())
                currentState = EnemyState.Attack;
            else
                currentState = EnemyState.Chase;

            if (nextStateTimer != null)
            {
                StopCoroutine(nextStateTimer);
                nextStateTimer = null;
                canSeek = true;
            }
        }
        else
        {
            if (path == null && nextStateTimer == null)
            {
                float time = Random.value * 10;
                nextStateTimer = StartCoroutine(SwitchToStateTimer(time, EnemyState.Idle));
            }
        }
    }

    float lastPathBuildTime;
    void ChaseState()
    {
        // Реализуйте логику состояния "Chase" (преследование игрока, когда игрок вне пределов "SeekRange").
        if (path != null)
        {
            PathFollow();
        }
        if ((path == null || Time.time - lastPathBuildTime > chaseBuildPathCooldown) && IsTargetVisible() && seeker.IsDone())
        {
            lastPathBuildTime = Time.time;
            seeker.StartPath(rb.position, enemyLocator.Target.position, OnPathCompleteSkipFirstWaypoint);
        }


        // Можно переключиться в другое состояние при выполнении определенного условия.
        if (IsTargetVisible())
        {
            if (IsTargetInGoodAttackRange())
            {
                ForgetPath();
                currentState = EnemyState.Attack;
            }
        }
        else
        {
            currentState = EnemyState.Seek;
        }
    }

    void AttackState()
    {
        // Реализуйте логику состояния "Attack" (атака игрока, когда игрок в пределах "AttackRange").
        if (canAttack)
        {
            if (IsTargetVisible())
            {
                // Direction Graphics Handling
                if (directionLookEnabled)
                {
                    if (enemyLocator.Target.position.x - transform.position.x > 0)
                    {
                        visuals.localScale = new Vector3(Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
                    }
                    else
                    {
                        visuals.localScale = new Vector3(-1f * Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
                    }
                }

                if (IsTargetInAttackRange())
                {
                    // Выполнить атаку.
                    animator.SetTrigger("Attack");
                    canAttack = false;
                    StartCoroutine(AttackCooldown());
                }
            }
        }

        // Можно переключиться в другое состояние при выполнении определенного условия.
        if (IsTargetVisible())
        {
            if (IsTargetInAttackRange())
                currentState = EnemyState.Attack;
            else
                currentState = EnemyState.Chase;
        }
        else
            currentState = EnemyState.Chase;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator SwitchToStateTimer(float afterTime, EnemyState toState)
    {
        yield return new WaitForSeconds(afterTime);
        currentState = toState;
        nextStateTimer = null;
    }

    private void PathFollow()
    {
        animator.SetTrigger("Walk");

        if (path == null) return;

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count) return;

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        // Movement
        rb.velocity = new Vector2(force.x, force.y);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            currentWaypoint++;

        if(currentWaypoint >= path.vectorPath.Count)
            ForgetPath();

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.005f)
            {
                visuals.localScale = new Vector3(Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
            }
            else if (rb.velocity.x < -0.005f)
            {
                visuals.localScale = new Vector3(-1f * Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
            }
        }
    }

    private void ForgetPath()
    {
        path = null;
        rb.velocity = Vector3.zero;
    }


    // Метод для определения, виден ли игрок.
    bool IsTargetVisible()
    {
        if (enemyLocator.Target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, enemyLocator.Target.position);
            return distanceToPlayer <= chaseRange;
        }
        return false;
    }

    // Метод для определения, игрок в пределах атаки.
    bool IsTargetInAttackRange()
    {
        if (enemyLocator.Target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, enemyLocator.Target.position);
            return distanceToPlayer <= attackRange;
        }
        return false;
    }

    // Метод для определения, игрок в пределах атаки.
    bool IsTargetInGoodAttackRange()
    {
        if (enemyLocator.Target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, enemyLocator.Target.position);
            return distanceToPlayer <= attackRange * multiplayerAttackRange;
        }
        return false;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnPathCompleteSkipFirstWaypoint(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    //Called from Attack Animation Event
    public void AttackPerfomed()
    {
        Debug.Log("Attaked");
        if(enemyLocator.TargetEntity != null)
            enemyLocator.TargetEntity.TakeDamage(enemyLocator.Target.position - transform.position, damage);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        GizmosDrawCircle(18, attackRange * multiplayerAttackRange);

        Gizmos.color = Color.red;
        GizmosDrawCircle(18, attackRange);

        Gizmos.color = Color.yellow;
        GizmosDrawCircle(36, chaseRange);
    }

    void GizmosDrawCircle(int segments, float radius)
    {
        float angleStep = 360.0f / segments; // Угол между каждым сегментом

        Vector3 prevPoint = Vector3.zero; // Предыдущая точка на окружности

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            Vector3 currentPoint = transform.position + new Vector3(x, y, 0);

            // Рисуем линию от предыдущей точки к текущей
            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, currentPoint);
            }

            prevPoint = currentPoint;
        }
    }
}
