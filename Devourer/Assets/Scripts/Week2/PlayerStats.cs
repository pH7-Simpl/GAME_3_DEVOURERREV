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
        animator.SetBool("died", true);
        GameObject.Find("Main Camera").transform.SetParent(null);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
