using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int maxPlayerHealth = 100;
    public int playerHealth = 0;
    private float showHBCooldown = 0f;
    private bool showHB = false;
    public bool hit = false;
    GameObject healthBar;
    [SerializeField] GameObject MC;
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Sword") {
            playerHealth -= 10;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
        }
    }

    void Start()
    {
        MC = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealth = maxPlayerHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
    }

    void FixedUpdate()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
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
    IEnumerator HitEffect(float duration)
    {
        hit = true;
        // Play hit effect animation or sound
        yield return new WaitForSeconds(duration);
        hit = false;
    }
    IEnumerator Die()
    {
        //MC.transform.SetParent(null);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        transform.position = new Vector2(transform.position.x, transform.position.y);
        animator.SetBool("died", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
