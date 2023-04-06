using System.Collections;
using UnityEngine;

public class LiftMechanism : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject player;
    private gameManager gm;
    private Slashing s;
    private GeyserSeedSpawn gss;
    private LightningDash ld;
    private Breathing b;
    private void Awake()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        gm.SetLift1(false);
        player = GameObject.FindGameObjectWithTag("Player");
        s = player.GetComponent<Slashing>();
        gss = player.GetComponent<GeyserSeedSpawn>();
        ld = player.GetComponent<LightningDash>();
        b = player.GetComponent<Breathing>();
        mainCamera = Camera.main.gameObject;
    }
    public void Lieft() {
        StartCoroutine(Lift1());
    }
    public void GoToBoss() {
        StartCoroutine(Lift2());
    }

    private IEnumerator Lift1()
    {
        gm.SetLift1(true);
        SetCripple(false);
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        Vector3 liftPos = transform.position + new Vector3(0, 0, -5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        float t = 0f;
        while (elapsedTime < duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(oriPos, liftPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(liftPos, oriPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        gm.SetLift1(false);
        SetCripple(true);
    }
    private IEnumerator Lift2() {
        gm.SetLift2(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SetCripple(false);
        Vector3 oriLiftPos = transform.localPosition;
        Vector3 endPos = transform.localPosition + new Vector3(0f, 26.547f, 0f);
        float elapsedTime = 0f;
        float duration = 3f;
        float t = 0f;
        while (elapsedTime < duration)
        {
            t = elapsedTime / duration;
            transform.localPosition = Vector2.Lerp(oriLiftPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
        yield return new WaitForSeconds(duration);
        gm.SetLift2(false);
    }
    private void SetCripple(bool x)
    {
        if(player != null) {
            player.GetComponent<PlayerMovement>().SetCanMove(x);
            player.GetComponent<Rigidbody2D>().velocity = (x) ? player.GetComponent<Rigidbody2D>().velocity : Vector2.zero;
        }
        mainCamera.GetComponent<MainCameraPlaying>().enabled = x;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
                if(enemyRB != null) {
                    enemyRB.velocity = (x) ? enemyRB.velocity : Vector2.zero;
                }
                EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
                EnemyAI enemy2 = enemy.GetComponent<EnemyAI>();
                if (enemy1 != null)
                {
                    enemy1.enabled = x;
                }
                if (enemy2 != null)
                {
                    enemy2.enabled = x;
                }
            }
        }
        s.SetCanSkill(x);
        gss.SetCanSkill(x);
        ld.SetCanSkill(x);
        b.SetCanSkill(x);
    }
}