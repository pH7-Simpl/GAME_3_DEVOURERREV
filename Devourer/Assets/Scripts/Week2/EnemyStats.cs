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
    private bool damaged;
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
            EnemyKnockBack(0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "FireBreath")
        {
            enemyHealth -= 1;
            EnemyTakesDamage(1.5f, 0);
        }
        if (other.gameObject.layer == 7)
        {
            EnemyKnockBack(0.1f);
        }
    }

    private void Awake()
    {
        maxEnemyHealth = 100;
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
    }
    private IEnumerator ShowHealthBar(float duration)
    {
        healthBar.SetActive(true);
        yield return new WaitForSeconds(duration);
        healthBar.SetActive(false);
    }
    public void EnemyTakesDamage(float duration, int damage) {
        StartCoroutine(EnemyHit(duration, damage));
    }
    private IEnumerator EnemyHit(float duration, int damage)
    {
        if(!damaged) {
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
    public void EnemyKnockBack(float duration) {
        StartCoroutine(Knockbacked(duration));
    }
    private IEnumerator Knockbacked(float duration)
    {
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
    }
    private IEnumerator colorForDamaged(float duration)
    {
        Color originalColor = GetComponent<SpriteRenderer>().color;
        Color targetColor = Color.red;
        float elapsedTime = 0f;
        float t = 0;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            GetComponent<SpriteRenderer>().color = Color.Lerp(targetColor, originalColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        GetComponent<SpriteRenderer>().color = originalColor;
    }
    private IEnumerator Die()
    {
        animator.SetBool("died", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<EnemyMovement>().enabled = false;
        yield return new WaitForSeconds(1f);
        ps.PFDE();
        Destroy(gameObject);
    }
}
