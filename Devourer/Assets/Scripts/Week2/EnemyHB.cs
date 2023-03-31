using UnityEngine;

public class EnemyHB : MonoBehaviour
{
    private GameObject enemy;
    private GameObject health;
    private EnemyStats es;

    private void Start()
    {
        enemy = transform.parent.gameObject;
        es = enemy.GetComponent<EnemyStats>();
        health = transform.Find("health").gameObject;
    }

    private void Update()
    {
        Vector3 healthScale = health.transform.localScale;
        healthScale.x = es.enemyHealth / 100f;
        health.transform.localScale = healthScale;
    }
}
