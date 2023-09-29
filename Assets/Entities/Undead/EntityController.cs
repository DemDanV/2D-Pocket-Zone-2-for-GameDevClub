using Pathfinding;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 8f;
    public float attackDistance = 1f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 3f;
    public float nextWaypointDistance = 3f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool directionLookEnabled = true;
    [SerializeField] Transform visuals;

    private Path path;
    private int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (path != null && currentWaypoint != path.vectorPath.Count - 1)
        {
            PathFollow();
        }
        else if (followEnabled)
            rb.velocity = Vector3.zero;
    }

    private void UpdatePath()
    {
        if (TargetInAttackRange())
        {
            Debug.Log("Attack");

            // Direction Graphics Handling
            if (directionLookEnabled)
            {
                if (target.transform.position.x - transform.position.x > 0)
                {
                    visuals.localScale = new Vector3(Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
                }
                else
                {
                    visuals.localScale = new Vector3(-1f * Mathf.Abs(visuals.localScale.x), visuals.localScale.y, visuals.localScale.z);
                }
            }
        }

        else if (followEnabled && TargetInDistance() && seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void PathFollow()
    {
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

    private bool NeedToMove()
    {
        bool inGoodRange = Vector2.Distance(transform.position, target.transform.position) < attackDistance * 0.75f;

        if (path != null)
        {
            if (inGoodRange)
                path = null;
        }

        return path == null && inGoodRange;
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private bool TargetInAttackRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) < attackDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
