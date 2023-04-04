using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private DoorMechanism dm;
    private bool doorIsOpened;
    private PlayerMovement pm;
    private gameManager gm;
    private GameObject keeptrack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(keeptrack != null) {
            Destroy(keeptrack);
            enemiesInArea.Remove(keeptrack);
        }
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Add(other.gameObject);
            Debug.Log(enemiesInArea.Count);
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
        keeptrack = new GameObject("Dummy");
        enemiesInArea.Add(keeptrack);
        dm = transform.GetChild(0).GetComponent<DoorMechanism>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gm = FindObjectOfType<gameManager>();
        doorIsOpened = false;
    }
    public bool EnemiesDefeated()
    {
        return enemiesInArea.Count == 0;
    }
    private void FixedUpdate()
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
