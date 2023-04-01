using UnityEngine;
using System.Collections;

public class GeyserSprouting : MonoBehaviour
{
    private float duration = 0.4f;
    private void Start()
    {
        gameObject.name = "waterSprout";
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D otherRB = other.GetComponent<Rigidbody2D>();
        if (other.gameObject.layer != 6 && other.gameObject.layer != 7 && otherRB != null)
        {
            if(other.gameObject.tag == "Enemy") {
                other.GetComponent<EnemyMovement>().SetSprouting(true);
            }
            otherRB.velocity += new Vector2(0, 20f);
            
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