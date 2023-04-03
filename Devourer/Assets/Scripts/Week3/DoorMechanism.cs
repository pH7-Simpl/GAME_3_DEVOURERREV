using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private bool opened;
    private GameObject roomCollider;
    private GameObject mainCamera;
    private Animator animator;
    private GameObject player;
    private gameManager gm;
    private Slashing s;
    private GeyserSeedSpawn gss;
    private LightningDash ld;
    private Breathing b;
    // Start is called before the first frame update
    private void Awake()
    {
        opened = false;
        animator = GetComponent<Animator>();
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        gm.SetDoorOpening(false);
        player = GameObject.FindGameObjectWithTag("Player");
        s = player.GetComponent<Slashing>();
        gss = player.GetComponent<GeyserSeedSpawn>();
        ld = player.GetComponent<LightningDash>();
        b = player.GetComponent<Breathing>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (opened)
        {
            StartCoroutine(openDoor());
            gm.SetDoorOpening(true);
            opened = false;
        }
    }

    private IEnumerator openDoor()
    {
        mainCamera = Camera.main.gameObject;
        MainCameraPlaying mcp = mainCamera.GetComponent<MainCameraPlaying>();
        mcp.enabled = false;
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        whileAnimation();
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
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        whileAnimation();
        yield return new WaitForSeconds(1f);
        elapsedTime = 0f;
        whileAnimation();
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
    }
    private void whileAnimation()
    {
        if (gm.IsSeeMap())
        {
            gm.SetSeeMap(false);
        }
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
        setSkillEnabledIfAlreadyUnlocked(false);
    }

    public void doorTest()
    {
        opened = true;
    }
    private void setSkillEnabledIfAlreadyUnlocked(bool x)
    {
        s.SetCanSkill(x);
        gss.SetCanSkill(x);
        ld.SetCanSkill(x);
        b.SetCanSkill(x);
    }
}