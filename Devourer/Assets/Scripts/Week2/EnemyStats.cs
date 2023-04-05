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
    private Color originalColor;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WindSlash")
        {
            EnemyTakesDamage(0.5f, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "LightningDash")
        {
            EnemyTakesDamage(0.5f, 10);
            Destroy(other.gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "FireBreath")
        {
            EnemyTakesDamage(1f, 1);
        }
        if (other.gameObject.tag == "Coll")
        {
            EnemyKnockBack(0.5f);
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
        originalColor = GetComponent<SpriteRenderer>().color;
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
        damaged = false;
        StartCoroutine(ShowHealthBar(duration));
        hit = true;
        animator.SetBool("hit", hit);
        yield return new WaitForSeconds(duration);
        hit = false;
        animator.SetBool("hit", hit);
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
        Color targetColor = Color.red;
        float elapsedTime = 0f;
        float t = 0;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            GetComponent<SpriteRenderer>().color = Color.Lerp(targetColor, originalColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
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
