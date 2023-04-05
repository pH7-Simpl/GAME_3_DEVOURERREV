using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LiftEnabled : MonoBehaviour
{
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private LiftMechanism lm;
    private bool liftEnabled;
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
        transform.GetChild(0).GetChild(0).GetComponent<GoToBoss>().enabled = false;
        playerEnteredRoom = false;
        enemySpawned = false;
        keeptrack = new GameObject("Dummy");
        enemiesInArea.Add(keeptrack);
        lm = transform.GetChild(0).GetComponent<LiftMechanism>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        gm = FindObjectOfType<gameManager>();
        liftEnabled = false;
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
            if (EnemiesDefeated() && !liftEnabled && pm.IsGrounded() && enemySpawned)
            {
                lm.Lieft();
                this.enabled = false;
                transform.GetChild(0).GetChild(0).GetComponent<Collider2D>().enabled = true;
                transform.GetChild(0).GetChild(0).GetComponent<GoToBoss>().enabled = true;
            }
        }

    }
    private IEnumerator spawnEnemy()
    {
        GetComponent<EnemySpawner>().SpawnEnemy();
        yield return new WaitForSeconds(Time.deltaTime);
        enemySpawned = true;
    }
}
