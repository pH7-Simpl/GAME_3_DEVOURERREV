using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float speed;
    private float nextWaypointDistance;
    private Transform enemyGFX;
    private Path path;
    private int currentWayPoint;
    private bool reachEndOfPath;
    private Seeker seeker;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 400f;
        nextWaypointDistance = 3f;
        currentWayPoint = 0;
        reachEndOfPath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyGFX = transform.Find("FlyEnemyGFX");

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnEndPath);
        }
    }
    private void OnEndPath(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        rb.AddForce(force);

        float dis = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (dis < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if (force.x >= 0.01)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
