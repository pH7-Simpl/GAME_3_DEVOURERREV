using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject swordCol;
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;
    // [SerializeField] private float jumpPower = 5f;
    [SerializeField] private float knockbackForce;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private EnemyStats es;

    private float horizontal;
    public bool isFacingRight = false;
    private bool attacking = false;
    [SerializeField] private bool foundPlayer = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
        es = GetComponent<EnemyStats>();
        healthBar = transform.GetChild(1).gameObject;
        speed = 4f;
        stopDistance = 2f;
        knockbackForce = 6f;
    }

    private void Update()
    {
        animator.SetFloat("moving", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (!attacking && player != null)
        {
            if (transform.position.x <= player.transform.position.x)
            {
                horizontal = 1;
            }
            else
            {
                horizontal = -1;
            }
            foundPlayer = false;
        }
        else
        {
            horizontal = 0f;
            rb2D.velocity = new Vector2(0f, 0f);
        }

        if (player != null && !es.hit)
        {
            if (!foundPlayer && Vector2.Distance(transform.position, player.transform.position) <= stopDistance && Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.5f)
            {
                foundPlayer = true;
                StartCoroutine(Attack());
            }
        }

        if (es.hit)
        {
            Knockback();
        }
        else
        {
            Move();
        }

        Flip();
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
    }

    private void Knockback()
    {
        rb2D.velocity = new Vector2(-horizontal * knockbackForce, rb2D.velocity.y);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            Vector3 healthScale = healthBar.transform.localScale;
            localScale.x *= -1f;
            healthScale.x *= -1f;
            transform.localScale = localScale;
            healthBar.transform.localScale = healthScale;
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        animator.SetBool("attacking", attacking);
        GameObject sword = null;
        if (isFacingRight)
        {
            sword = Instantiate(swordCol, transform.position + new Vector3(1.25f, 0.065f), transform.rotation);
        }
        else
        {
            sword = Instantiate(swordCol, transform.position + new Vector3(-1.25f, 0.065f), transform.rotation);
        }
        Destroy(sword, Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        animator.SetBool("attacking", attacking);
    }
}
