using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private DoorMechanism dm;
    private bool doorIsOpened;
    private PlayerMovement pm;
    private gameManager gm;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Remove(other.gameObject);
        }
    }
    private void Awake()
    {
        dm = transform.GetChild(0).GetComponent<DoorMechanism>();
        doorIsOpened = false;
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gm = FindObjectOfType<gameManager>();
    }
    public bool EnemiesDefeated()
    {
        return enemiesInArea.Count == 0;
    }
    private void Update()
    {
        if (gm.GetGameOver())
        {
            return;
        }
        if (EnemiesDefeated() && !doorIsOpened && pm.IsGrounded())
        {
            dm.openDoer();
            doorIsOpened = false;
            this.enabled = false;
        }
    }
}
