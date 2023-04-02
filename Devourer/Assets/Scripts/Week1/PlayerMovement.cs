using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpPower = 5f;
    public float GetJumpPower()
    {
        return jumpPower;
    }
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private float doublePressTime = 0.2f;
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private bool dashUpgrade = false;
    public void SetDashUpgrade(bool x)
    {
        dashUpgrade = x;
    }
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject healthBar;

    private float horizontal;
    private bool isFacingRight = true;
    public bool IsFacingRight()
    {
        return isFacingRight;
    }
    private bool jumping;
    private int jumpsRemaining;
    private float lastPressTime;
    private bool canDash;
    private bool isDashing;
    private PlayerStats ps;
    private bool bombHitExecuted;

    private void Start()
    {
        jumpsRemaining = maxJumps;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.transform.SetParent(transform);
        ps = GetComponent<PlayerStats>();
        healthBar = transform.GetChild(1).gameObject;
        canDash = true;
        bombHitExecuted = false;
    }


    private void Update()
    {
        if (isDashing || ps.IsHit())
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        Jump();

        if (!dashUpgrade)
        {
            Dashing();
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (ps.IsHit())
        {
            GameObject sword = GameObject.FindGameObjectWithTag("Sword");
            if (sword != null)
            {
                GameObject enemy = sword.transform.parent.gameObject;
                EnemyMovement em = enemy.GetComponent<EnemyMovement>();
                if (em != null)
                {
                    float xDirection = em.IsFacingRight() ? knockbackForce : -knockbackForce;
                    rb2D.velocity = new Vector2(xDirection, rb2D.velocity.y);
                }
            }
            if (!bombHitExecuted)
            {
                StartCoroutine(callBombCheck());
            }
        }
        else
        {
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
        }
    }
    private IEnumerator callBombCheck()
    {
        GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
        if (bomb != null && !bombHitExecuted)
        {
            float xDirection = (transform.position.x <= bomb.transform.position.x) ? knockbackForce : -knockbackForce;
            float yDirection = (transform.position.y <= bomb.transform.position.y) ? -knockbackForce : knockbackForce;
            rb2D.velocity = new Vector2(xDirection, yDirection);
        }
        bombHitExecuted = true;
        yield return new WaitForSeconds(Time.deltaTime);
        bombHitExecuted = false;

    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);
    }

    private void Flip()
    {
        bool shouldFlip = (isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f);
        if (shouldFlip)
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

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            float yDirection = (jumpsRemaining == maxJumps) ? jumpPower : jumpPower * 1.1f;
            rb2D.velocity = new Vector2(rb2D.velocity.x, yDirection);
            jumpsRemaining--;
        }
        if (IsGrounded() && jumpsRemaining <= 0)
        {
            jumpsRemaining = maxJumps;
        }
        jumping = !IsGrounded();
        animator.SetBool("isJumping", jumping);
    }
    private void Dashing()
    {
        bool doublePressed = (Time.time - lastPressTime) < doublePressTime;

        if (canDash && doublePressed)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RegularDash(1));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RegularDash(-1));
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            lastPressTime = Time.time;
        }
    }
    private IEnumerator RegularDash(int direction)
    {
        canDash = false;
        isDashing = true;
        rb2D.gravityScale = 0f;
        rb2D.velocity = new Vector2(direction * dashPower, 0);
        yield return new WaitForSeconds(dashingTime);
        rb2D.gravityScale = 1f;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
