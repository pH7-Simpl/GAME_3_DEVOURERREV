using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    public float jumpPower = 5f;
    public bool isFacingRight = true;
    private bool jumping;
    public int maxJumps = 1;
    private int jumpsRemaining;
    private float lastPressTime;
    private float doublePressTime = 0.2f;
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    public bool dashUpgrade = false;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public Animator animator;
    private void Start()
    {
        jumpsRemaining = maxJumps;
        GameObject.Find("Main Camera").transform.SetParent(transform);
    }


    void Update()
    {
        if (isDashing)
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
        rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            if (jumpsRemaining == maxJumps)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
            }
            else
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower * 1.1f);
            }
            jumpsRemaining--;
        }
        if (isGrounded() && jumpsRemaining <= 0)
        {
            jumpsRemaining = maxJumps;
        }
        if (isGrounded())
        {
            jumping = false;
        }
        else
        {
            jumping = true;
        }
        animator.SetBool("isJumping", jumping);
    }
    private void Dashing()
    {
        if (Input.GetKeyDown(KeyCode.D) && (Time.time - lastPressTime) < doublePressTime && canDash)
        {
            StartCoroutine(RegularDash(1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastPressTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.A) && (Time.time - lastPressTime) < doublePressTime && canDash)
        {
            StartCoroutine(RegularDash(-1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastPressTime = Time.time;
        }
    }
    private IEnumerator RegularDash(int direction)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0f;
        rb2D.velocity = new Vector2(direction * dashPower, 0);
        yield return new WaitForSeconds(dashingTime);
        rb2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
