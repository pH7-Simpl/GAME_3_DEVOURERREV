using UnityEngine;

public class EnemyHBAI : MonoBehaviour
{
    private GameObject enemy;
    private GameObject health;
    private EnemyAI eAI;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        eAI = enemy.GetComponent<EnemyAI>();
        health = transform.Find("health").gameObject;
    }

    private void Update()
    {
        Vector3 healthScale = health.transform.localScale;
        healthScale.x = eAI.GetEnemyHealth() / 100f;
        health.transform.localScale = healthScale;
    }
}
