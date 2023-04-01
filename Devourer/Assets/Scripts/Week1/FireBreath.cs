using UnityEngine;

public class FireBreath : MonoBehaviour
{
    private float breathDuration;
    private GameObject player;
    private PlayerMovement pm;
    private void Start()
    {
        breathDuration = 1.5f;
        player = GameObject.Find("Player");
        transform.SetParent(player.transform);
        pm = player.GetComponent<PlayerMovement>();
        transform.gameObject.name = "fireBreath";
        if (!pm.IsFacingRight())
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void Update()
    {
        if (breathDuration >= 0f)
        {
            breathDuration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
