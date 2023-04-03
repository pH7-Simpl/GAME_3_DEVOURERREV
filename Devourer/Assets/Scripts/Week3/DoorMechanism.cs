using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private bool opened;
    private GameObject roomCollider;
    private GameObject mainCamera;
    private Animator animator;
    private GameObject player;
    gameManager gm;
    // Start is called before the first frame update
    private void Awake()
    {
        opened = false;
        animator = GetComponent<Animator>();
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (opened && !gm.IsSeeMap())
        {
            StartCoroutine(openDoor());
        }
    }

    private IEnumerator openDoor()
    {
        mainCamera = Camera.main.gameObject;
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -5f);
        Vector3 camPosBeforeDoor = mainCamera.transform.position;
        whileAnimation();
        Vector3 doorPos = transform.position + new Vector3(0, 0, -5f);
        float lerpTime = 0f;
        float lerpDuration = 0.5f;
        while (lerpTime < 0.5f)
        {
            lerpTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(oriPos, doorPos, lerpTime / lerpDuration);
            yield return null;
        }
        animator.SetBool("opened", opened);
        GetComponent<BoxCollider2D>().enabled = false;
        whileAnimation();
        yield return new WaitForSeconds(1f);
        lerpTime = 0f;
        whileAnimation();
        while (lerpTime < 0.5f)
        {
            lerpTime += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(doorPos, oriPos, lerpTime / lerpDuration);
            yield return null;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        opened = false;
    }
    private void whileAnimation() {
        if (gm.IsSeeMap())
        {
            gm.SetSeeMap(false);
        }
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    public void doorTest() {
        opened = true;
    }
}