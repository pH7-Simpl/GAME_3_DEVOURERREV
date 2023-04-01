using UnityEngine;

public class GeyserSeed : MonoBehaviour
{
    [SerializeField] private GameObject waterGeyser;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            Instantiate(waterGeyser, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.05f, wallLayer);
    }
}