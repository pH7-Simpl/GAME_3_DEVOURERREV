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
    private Vector2 dir;
    private Vector2 force;
    private float dis;
    private float maxEnemyHealth;
    private float enemyHealth;
    public float GetEnemyHealth() {
        return enemyHealth;
    }
    private bool showHB;
    private bool hit;
    public bool IsHit() {
        return hit;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "windSlash")
        {
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "lightningDash")
        {
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Player") {
            Debug.Log("Test");
        }
    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 400f;
        nextWaypointDistance = 3f;
        currentWayPoint = 0;
        reachEndOfPath = false;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyGFX = transform.Find("FlyEnemyGFX");
        enemyHealth = 100f;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        showHB = false;
        maxEnemyHealth = 100f;
        enemyHealth = maxEnemyHealth;
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

        dir = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        force = dir * speed * Time.deltaTime;

        rb.AddForce(force);

        dis = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

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
        if(reachEndOfPath) {
            Debug.Log("test");
        }
    }
    public IEnumerator HitEffect(float duration)
    {
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
    }
}
