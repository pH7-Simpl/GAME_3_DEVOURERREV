using UnityEngine;

public class Explosion : MonoBehaviour
{
    private GameObject player;
    private PlayerStats ps;
    private bool executed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerStats>();
        executed = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player" && !executed)
        {
            executed = true;
            ps.SetHit(true);
            ps.playerHealth -= 25;
            ps.SetShowHB(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            ps.SetHit(false);
            ps.SetShowHB(true);
        }
    }
}
