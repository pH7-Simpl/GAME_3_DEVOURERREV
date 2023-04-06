using UnityEngine;
using System.Collections;

public class waterStrike : MonoBehaviour
{
    private float duration = 0.5f;
    private void Start()
    {
        gameObject.name = "waterStrike";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
        if (other.gameObject.layer != 6 && other.gameObject.layer != 7 && otherRB != null)
        {
            if (other.CompareTag("Player"))
            {
                otherRB.velocity += new Vector2(0, 20f);
            }
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