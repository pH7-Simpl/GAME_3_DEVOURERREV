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
    private bool hit;
    private GameObject healthBar;
    [SerializeField] private GameObject detonator;
    [SerializeField] private Animator animator;
    private PlayerStats ps;
    private bool damaged;
    private SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WindSlash")
        {
            EnemyTakesDamage(0.5f, 25);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "LightningDash")
        {
            EnemyTakesDamage(0.5f, 25);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player" || other.gameObject.name == "FireBreath")
        {
            StartCoroutine(Explode());
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            EnemyKnockBack(0.1f);
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
        maxEnemyHealth = 100f;
        enemyHealth = maxEnemyHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
        animator = transform.GetChild(0).GetComponent<Animator>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damaged = false;
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
    private IEnumerator ShowHealthBar(float duration)
    {
        healthBar.SetActive(true);
        yield return new WaitForSeconds(duration);
        healthBar.SetActive(false);
    }
    public void EnemyTakesDamage(float duration, int damage)
    {
        StartCoroutine(EnemyHit(duration, damage));
    }
    private IEnumerator EnemyHit(float duration, int damage)
    {
        if (!damaged)
        {
            StartCoroutine(colorForDamaged(duration));
            enemyHealth -= damage;
            damaged = true;
        }
        StartCoroutine(ShowHealthBar(duration));
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
        damaged = false;
    }
     private IEnumerator colorForDamaged(float duration)
    {
        Color originalColor = sr.color;
        Color targetColor = Color.red;
        float elapsedTime = 0f;
        float t = 0;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            sr.color = Color.Lerp(targetColor, originalColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sr.color = originalColor;
    }
    public void EnemyKnockBack(float duration) {
        StartCoroutine(Knockbacked(duration));
    }
    private IEnumerator Knockbacked(float duration)
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
        ps.PFDE();
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
}
