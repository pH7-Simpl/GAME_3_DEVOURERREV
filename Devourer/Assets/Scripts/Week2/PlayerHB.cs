using UnityEngine;

public class PlayerHB : MonoBehaviour
{
    private GameObject Player;
    private GameObject Health;
    private PlayerStats ps;

    private void Start()
    {
        Player = transform.parent.gameObject;
        ps = Player.GetComponent<PlayerStats>();
        Health = transform.Find("health").gameObject;
    }

    private void Update()
    {
        Vector3 healthScale = Health.transform.localScale;
        healthScale.x = ps.playerHealth / 100f;
        Health.transform.localScale = healthScale;
    }
}
