using UnityEngine;

public class GeyserSprouting : MonoBehaviour
{
    private float duration = 0.4f;
    private void Start()
    {
        gameObject.name = "waterSprout";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 6 && other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, 20f);
        }
    }
    private void FixedUpdate()
    {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
    }
}