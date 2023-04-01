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
    private bool isFacingRight = false;
    private Vector2 force;
    private Vector2 dir;
    public bool IsFacingRight()
    {
        return isFacingRight;
    }
    private bool attacking = false;
    [SerializeField] private bool foundPlayer = false;
    [SerializeField] private bool sprouting;
    public void SetSprouting(bool x) {
        sprouting = x;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
        es = GetComponent<EnemyStats>();
        healthBar = transform.GetChild(1).gameObject;
        speed = 200f;
        stopDistance = 2f;
        knockbackForce = 3f;
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

        if (player != null && !es.IsHit())
        {
            if (!foundPlayer && Vector2.Distance(transform.position, player.transform.position) <= stopDistance && Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.5f)
            {
                foundPlayer = true;
                StartCoroutine(Attack());
            }
        }

        if (es.IsHit())
        {
            Knockback();
        }
        else if(sprouting)
        {
            StartCoroutine(SproutDelay());
        }
        else
        {
            Move();
        }
        Flip();
    }
    private IEnumerator SproutDelay() {
        yield return new WaitForSeconds(1f);
        sprouting = false;
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);
    }

    private void Move()
    {
        if(player != null) {
            dir = (horizontal > 0) ? Vector2.right : Vector2.left;
            if(IsGrounded()) {
                rb2D.gravityScale = 0f;
                dir.y = 0f;
                force = dir * speed * Time.deltaTime;
                rb2D.velocity = force;
            } else {
                rb2D.gravityScale = 2f;
            }
        } else {
            rb2D.velocity = Vector2.zero;
        }
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
