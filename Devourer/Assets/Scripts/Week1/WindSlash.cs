using UnityEngine;

public class WindSlash : MonoBehaviour
{
    private float speed = 10f;
    private GameObject player;
    private PlayerMovement pm;
    private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.gameObject.name = "WindSlash";

        if (pm.IsFacingRight())
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 50f)
        {
            Destroy(gameObject);
        }
    }
}
