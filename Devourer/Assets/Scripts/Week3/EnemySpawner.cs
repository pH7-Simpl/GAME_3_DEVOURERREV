using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemy1Count;
    [SerializeField] private int enemy2Count;
    [SerializeField] private int enemy3Count;
    [SerializeField] private Vector3[] enemy1Pos;
    [SerializeField] private Vector3[] enemy2Pos;
    [SerializeField] private Vector3[] enemy3Pos;
    public void SpawnEnemy() {
        for (int i = 0; i < enemy1Count; i++)
        {
            Instantiate(enemyPrefabs[0], enemy1Pos[i], Quaternion.identity);
        }
        for (int i = 0; i < enemy2Count; i++)
        {
            Instantiate(enemyPrefabs[1], enemy2Pos[i], Quaternion.identity);
        }
        for (int i = 0; i < enemy3Count; i++)
        {
            GameObject boss = Instantiate(enemyPrefabs[2], enemy3Pos[i], Quaternion.identity);
            boss.name = "Boss";
        }
    }
}
