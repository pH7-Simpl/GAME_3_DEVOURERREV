using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : EnemySpawner
{
    GameObject player;
    private bool leftRoom;
    private void Awake() {
        leftRoom = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(leftRoom) {
            SpawnEnemy();
            leftRoom = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            leftRoom = true;
        }
    }
}
