using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : BossBehaviour
{
    private int maxBossHealth;
    [SerializeField] private int bossHealth;
    private PlayerStats ps;
    private bool damaged;
    private Color originalColor;
    private bool died;
    public bool GetDied() {
        return died;
    }
    private void Awake() {
        bs = this;
        maxBossHealth = 200;
        bossHealth = maxBossHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        s = player.GetComponent<Slashing>();
        gss = player.GetComponent<GeyserSeedSpawn>();
        ld = player.GetComponent<LightningDash>();
        b = player.GetComponent<Breathing>();
        mainCamera = Camera.main.gameObject;
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
        SetCripple(false);
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        Vector3 bossPos = transform.position + new Vector3(0, 0, -5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        float t = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(oriPos, bossPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = bossPos;
        animator.SetBool("died", died);
        yield return new WaitForSeconds(2f);
        elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(bossPos, oriPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(player != null) {
            player.GetComponent<PlayerMovement>().enabled = true;
        }
        SetCripple(true);
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
        if (bossHealth <= 0 && !died)
        {
            ps.PFBD();
            StartCoroutine(Die());
        }
    }
}
