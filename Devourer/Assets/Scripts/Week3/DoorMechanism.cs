using System.Collections;
using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private bool yetOpened;
    private GameObject roomCollider;
    private GameObject mainCamera;
    private Animator animator;
    private GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        yetOpened = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (yetOpened)
        {
            StartCoroutine(openDoor());
        }
    }
    private IEnumerator openDoor()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main.gameObject;
        Vector3 oriPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 doorPos = transform.position + new Vector3(0, 0, -5);
        float lerpTime = 0f;
        float lerpDuration = 0.5f;
        while (lerpTime < lerpDuration)
        {
            lerpTime += Time.unscaledDeltaTime;
            mainCamera.transform.position = Vector3.Lerp(oriPos, doorPos, lerpTime / lerpDuration);
            yield return null;
        }
        animator.SetBool("opened", yetOpened);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        lerpTime = 0f;
        while (lerpTime < lerpDuration)
        {
            lerpTime += Time.unscaledDeltaTime;
            mainCamera.transform.position = Vector3.Lerp(doorPos, oriPos, lerpTime / lerpDuration);
            yield return null;
        }
        player.GetComponent<PlayerMovement>().enabled = true;
        mainCamera.transform.position = oriPos;
        yetOpened = false;
    }

}
