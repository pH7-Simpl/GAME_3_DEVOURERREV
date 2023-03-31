using UnityEngine;

public class DownSlash : MonoBehaviour
{
    private float speed = 10f;
    GameObject player;
    PlayerMovement pm;
    Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.transform.position.x, pm.jumpPower * 1.1f);
        transform.gameObject.name = "windSlash";
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y - player.transform.position.y) > 10f)
        {
            Destroy(gameObject);
        }
    }
}