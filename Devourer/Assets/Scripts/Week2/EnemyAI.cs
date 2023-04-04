using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
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
    public float GetEnemyHealth()
    {
        return enemyHealth;
    }
    private bool showHB;
    private bool hit;
    private GameObject healthBar;
    private float showHBCooldown;
    private bool hasCollided;
    [SerializeField] private GameObject detonator;
    [SerializeField] private Animator animator;
    private PlayerStats ps;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided)
        {
            return;
        }
        if (other.gameObject.name == "WindSlash")
        {
            Destroy(other.gameObject);
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
        }
        if (other.gameObject.name == "LightningDash")
        {
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player" || other.gameObject.name == "FireBreath")
        {
            StartCoroutine(Explode());
        }
        StartCoroutine(ColliderRefresher());
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            StartCoroutine(HitEffect(0.1f));
        }
        if (other.gameObject.layer == 6)
        {
            if (transform.position.y >= other.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, 1f);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -1f);
            }
        }
    }
    private IEnumerator ColliderRefresher()
    {
        hasCollided = true;
        yield return new WaitForSeconds(Time.deltaTime);
        hasCollided = false;
    }
    private void Awake()
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
        hit = false;
        showHB = false;
        showHBCooldown = 0f;
        maxEnemyHealth = 100f;
        enemyHealth = maxEnemyHealth;
        healthBar = transform.GetChild(1).gameObject;
        hasCollided = false;
        animator = transform.GetChild(0).GetComponent<Animator>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
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
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
        }
    }
    void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            StartCoroutine("Die");
        }
        ShowHealthBar();
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

        if (!hit)
        {
            rb.AddForce(force);
        }
        else
        {
            if (force.x >= 0.01)
            {
                rb.velocity = new Vector2(-3f, 0f);
            }
            else if (force.x <= -0.01)
            {
                rb.velocity = new Vector2(3f, 0f);
            }
        }

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
        if (reachEndOfPath)
        {
            Debug.Log("test");
        }
    }
    public IEnumerator HitEffect(float duration)
    {
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
    }
    private IEnumerator Die()
    {
        animator.SetBool("died", true);
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        yield return new WaitForSeconds(1f);
        ps.PFRE();
        Destroy(gameObject);
    }

    private IEnumerator Explode()
    {
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        animator.SetBool("exploded", true);
        GameObject bomb = Instantiate(detonator, transform.position, Quaternion.identity);
        Destroy(bomb, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void ShowHealthBar()
    {
        if (showHB)
        {
            showHBCooldown = 1f;
            healthBar.SetActive(true);
            showHB = false;
        }
        if (showHBCooldown >= 0f)
        {
            showHBCooldown -= Time.deltaTime;
        }
        else
        {
            healthBar.SetActive(false);
        }
    }
}
