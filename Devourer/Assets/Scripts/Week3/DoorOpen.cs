using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private DoorMechanism dm;
    private bool doorIsOpened;
    private PlayerMovement pm;
    private gameManager gm;
    private GameObject keeptrack;
    private bool playerEnteredRoom;
    private bool instansiated;
    private bool enemySpawned;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            playerEnteredRoom = true;
        }
        if (keeptrack != null)
        {
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
        playerEnteredRoom = false;
        enemySpawned = false;
        keeptrack = new GameObject("Dummy");
        enemiesInArea.Add(keeptrack);
        dm = transform.GetChild(0).GetComponent<DoorMechanism>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gm = FindObjectOfType<gameManager>();
        doorIsOpened = false;
        instansiated = false;
    }
    public bool EnemiesDefeated()
    {
        return enemiesInArea.Count == 0;
    }
    private void FixedUpdate()
    {
        if (!gm.GetGameOver() && playerEnteredRoom)
        {
            if (playerEnteredRoom && !instansiated)
            {
                StartCoroutine(spawnEnemy());
                instansiated = true;
            }
            if (EnemiesDefeated() && !doorIsOpened && pm.IsGrounded() && enemySpawned)
            {
                dm.openDoer();
                this.enabled = false;
            }
        }

    }
    private IEnumerator spawnEnemy()
    {
        
        yield return new WaitForSeconds(Time.deltaTime);
        enemySpawned = true;
    }
}
