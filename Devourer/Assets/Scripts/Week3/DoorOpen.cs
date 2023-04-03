using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private List<GameObject> enemiesInArea = new List<GameObject>();
    private GameObject relatedDoor;
    private DoorMechanism dm;
    private bool openingDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        { 
            enemiesInArea.Add(other.gameObject);
        }
    }
    private void Awake() {
        relatedDoor = GameObject.FindGameObjectWithTag("Door");
        dm = relatedDoor.GetComponent<DoorMechanism>();
        openingDoor = false;
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInArea.Remove(other.gameObject);
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        return enemiesInArea.Count == 0;
    }
    private void Update() {
        if(AreAllEnemiesDefeated() && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsGrounded()) {
            if(!openingDoor) {
                dm.SetOpened(true);
                openingDoor = true;
            }
        }
    }
}
