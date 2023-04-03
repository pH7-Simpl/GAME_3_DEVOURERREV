using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int maxEnemyHealth;
    private int enemyHealth;
    public int GetEnemyHealth()
    {
        return enemyHealth;
    }
    private float showHBCooldown;
    private bool showHB = false;
    private bool hit = false;
    public bool IsHit()
    {
        return hit;
    }
    private GameObject healthBar;
    private Animator animator;
    private EnemyMovement em;
    private Rigidbody2D rb;
    private PlayerStats ps;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WindSlash")
        {
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "LightningDash")
        {
            enemyHealth -= 25;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 6)
        {
            if (transform.position.y <= other.transform.position.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(rb.velocity.y) / 2);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, other.transform.position.y + 1f, transform.position.z);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
        if (other.gameObject.tag == "Player")
        {
            rb.gravityScale = 0f;
            StartCoroutine(HitEffect(0.5f));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "FireBreath")
        {
            enemyHealth -= 1;
            showHB = true;
            StartCoroutine(HitEffect(1.5f));
        }
        if (other.gameObject.layer == 7)
        {
            StartCoroutine(HitEffect(0.1f));
        }
    }

    private void Awake()
    {
        maxEnemyHealth = 100;
        showHBCooldown = 0f;
        enemyHealth = maxEnemyHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        em = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
        }
    }

    private void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            StartCoroutine("Die");
        }
        ShowHealthBar();
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

    public IEnumerator HitEffect(float duration)
    {
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
    }
    private IEnumerator Die()
    {
        animator.SetBool("died", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<EnemyMovement>().enabled = false;
        yield return new WaitForSeconds(1f);
        ps.PlayerGetPoints(10);
        Destroy(gameObject);
    }
}
