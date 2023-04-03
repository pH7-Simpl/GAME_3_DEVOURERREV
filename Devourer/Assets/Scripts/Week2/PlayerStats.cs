using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int maxPlayerHealth = 100;
    public int playerHealth = 0;
    private float showHBCooldown = 0f;
    private bool showHB = false;
    public void SetShowHB(bool x) {
        showHB = x;
    }
    private bool hit = false;
    public void SetHit(bool x) {
        hit = x;
    }
    public bool IsHit()
    {
        return hit;
    }

    GameObject healthBar;
    [SerializeField] GameObject MC;
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            playerHealth -= 10;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
        }
    }

    private void Start()
    {
        maxPlayerHealth = 100;
        showHBCooldown = 0f;
        MC = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealth = maxPlayerHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
        }
    }

    private void FixedUpdate()
    {
        if (playerHealth <= 0)
        {
            StartCoroutine(Die());
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetBool("died", true);
        yield return new WaitForSeconds(1f);
        MC.transform.SetParent(null);
        FindObjectOfType<gameManager>().SetGameOver(true);
        Destroy(gameObject);
    }
}
