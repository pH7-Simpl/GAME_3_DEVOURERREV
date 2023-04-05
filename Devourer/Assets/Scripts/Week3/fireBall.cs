using UnityEngine;

public class fireBall : MonoBehaviour
{
    private float speed = 10f;
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
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }
}
