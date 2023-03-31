using UnityEngine;

public class GeyserSeed : MonoBehaviour
{
    public GameObject waterGeyser;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;
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