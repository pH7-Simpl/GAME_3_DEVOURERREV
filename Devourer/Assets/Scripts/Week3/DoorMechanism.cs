using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
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
        mainCamera = Camera.main.gameObject;
    }
    public void openDoer() {
        StartCoroutine(openDoor());
    }

    private IEnumerator openDoor()
    {
        gm.SetDoorOpening(true);
        SetCripple(false);
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        Vector3 doorPos = transform.position + new Vector3(0, 0, -5f);
        float elapsedTime = 0f;
        float duration = 0.5f;
        float t = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(oriPos, doorPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("opened", true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            mainCamera.transform.position = Vector3.Lerp(doorPos, oriPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gm.SetDoorOpening(false);
        SetCripple(true);
    }
    private void SetCripple(bool x)
    {
        if(player != null) {
            player.GetComponent<PlayerMovement>().SetCanMove(x);
        }
        mainCamera.GetComponent<MainCameraPlaying>().enabled = x;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
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