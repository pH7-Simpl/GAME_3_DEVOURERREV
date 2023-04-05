using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    private int maxBossHealth;
    private int bossHealth;
    private Animator animator;
    private BossBehaviour bb;
    private PlayerStats ps;
    private bool damaged;
    private Color originalColor;
    private bool died;
    public bool GetDied() {
        return died;
    }
    private void Awake() {
        maxBossHealth = 200;
        bossHealth = maxBossHealth;
        animator = GetComponent<Animator>();
        bb = GetComponent<BossBehaviour>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damaged = false;
        originalColor = originalColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WindSlash")
        {
            BossTakesDamage(0.5f, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "LightningDash")
        {
            BossTakesDamage(0.5f, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Coll")
        {
            ps.PlayerTakesDamage(0.5f, 20);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "FireBreath")
        {
            BossTakesDamage(1f, 1);
        }
    }
    public void BossTakesDamage(float duration, int damage) {
        StartCoroutine(BossHit(duration, damage));
    }
    private IEnumerator BossHit(float duration, int damage)
    {
        if(!damaged) {
            StartCoroutine(colorForDamaged(duration));
            bossHealth -= damage;
            damaged = true;
        }
        damaged = false;
        animator.SetBool("hit", true);
        yield return new WaitForSeconds(duration);
        animator.SetBool("hit", false);
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
        died = true;
        animator.SetBool("died", died);
        yield return new WaitForSeconds(1f);
        ps.PFDB();
        Destroy(gameObject);
    }



    private void Update()
    {
        if (bossHealth <= 0)
        {
            bossHealth = 0;
        }
    }

    private void FixedUpdate()
    {
        if (bossHealth <= 0)
        {
            StartCoroutine("Die");
        }
    }
}
