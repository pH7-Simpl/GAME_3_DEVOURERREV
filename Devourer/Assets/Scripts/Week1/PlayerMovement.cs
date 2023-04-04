using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    public float GetJumpPower()
    {
        return jumpPower;
    }
    [SerializeField] private int maxJumps;
    [SerializeField] private float doublePressTime;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;
    [SerializeField] private float knockbackForce;
    [SerializeField] private bool dashUpgrade;
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
    private bool isFacingRight;
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

    private void Awake()
    {
        speed = 8f;
        jumpPower = 8f;
        maxJumps = 1;
        doublePressTime = 0.2f;
        dashPower = 10f;
        dashingTime = 0.2f;
        dashingCooldown = 1f;
        knockbackForce = 10f;
        jumpsRemaining = maxJumps;
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.position = transform.position + new Vector3(0f, 0f, -5f);
        mainCamera.transform.SetParent(transform);
        ps = GetComponent<PlayerStats>();
        healthBar = transform.GetChild(1).gameObject;
        dashUpgrade = false;
        canDash = true;
        bombHitExecuted = false;
        isFacingRight = true;
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
                bomb();
                bombHitExecuted = true;
            }
        }
        else
        {
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
        }
    }
    private void bomb() {
        StartCoroutine(callBombCheck());
    }
    private IEnumerator callBombCheck()
    {
        GameObject bomb = GameObject.FindGameObjectWithTag("Bomb");
        if (bomb != null && !bombHitExecuted)
        {
            Vector2 knockbackDirection = (transform.position - bomb.transform.position).normalized;
            rb2D.velocity = knockbackDirection * knockbackForce;
        }
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
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        if(isFacingRight) {
            healthBar.transform.localScale = new Vector3(1f, 1f, 1f);
        } else {
            healthBar.transform.localScale = new Vector3(-1f, 1f, 1f);
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
        rb2D.gravityScale = 2f;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
