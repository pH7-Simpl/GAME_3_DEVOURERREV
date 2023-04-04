using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    private GameObject roomCollider;
    private GameObject mainCamera;
    private Animator animator;
    private GameObject player;
    private gameManager gm;
    private Slashing s;
    private GeyserSeedSpawn gss;
    private LightningDash ld;
    private Breathing b;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        gm.SetDoorOpening(false);
        player = GameObject.FindGameObjectWithTag("Player");
        s = player.GetComponent<Slashing>();
        gss = player.GetComponent<GeyserSeedSpawn>();
        ld = player.GetComponent<LightningDash>();
        b = player.GetComponent<Breathing>();
    }
    public void openDoer() {
        StartCoroutine(openDoor());
    }

    private IEnumerator openDoor()
    {
        gm.SetDoorOpening(true);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
                EnemyAI enemy2 = enemy.GetComponent<EnemyAI>();
                if (enemy1 != null)
                {
                    enemy1.enabled = false;
                }
                if (enemy2 != null)
                {
                    enemy2.enabled = false;
                }
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        setSkillEnabledIfAlreadyUnlocked(false);
        mainCamera = Camera.main.gameObject;
        MainCameraPlaying mcp = mainCamera.GetComponent<MainCameraPlaying>();
        mcp.enabled = false;
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        Vector3 doorPos = transform.position + new Vector3(0, 0, -5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        float t = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(oriPos, doorPos, t);
            yield return new WaitForSeconds(Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        animator.SetBool("opened", true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(doorPos, oriPos, t);
            yield return new WaitForSeconds(Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        gm.SetDoorOpening(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        setSkillEnabledIfAlreadyUnlocked(true);
        mcp.enabled = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy !=null)
            {
                EnemyMovement enemy1 = enemy.GetComponent<EnemyMovement>();
                EnemyAI enemy2 = enemy.GetComponent<EnemyAI>();
                if (enemy1 != null)
                {
                    enemy1.enabled = true;
                }
                if (enemy2 != null)
                {
                    enemy2.enabled = true;
                }
            }
        }
    }
    private void setSkillEnabledIfAlreadyUnlocked(bool x)
    {
        s.SetCanSkill(x);
        gss.SetCanSkill(x);
        ld.SetCanSkill(x);
        b.SetCanSkill(x);
    }
}