using UnityEngine;
using UnityEngine.UI;

public class LightningDash : MonoBehaviour
{
    private float lastPressTime;
    private float doublePressTime;
    private float dashLength;
    private float cooldownDuration;
    private float currentCooldown;
    private float animationCooldown;
    public GameObject lightningDashPrefab;
    private Image coolDownImage;
    private GameObject theDash = null;
    private GameObject player;
    private PlayerMovement pm;
    private int wallLayerMask; // layer mask for walls

    void Start()
    {
        player = GameObject.Find("Player");
        pm = GetComponent<PlayerMovement>();
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/LightningSkill/Cooldown").GetComponent<Image>();
        doublePressTime = 0.2f;
        dashLength = 6f;
        currentCooldown = 0f;
        animationCooldown = 0f;
        cooldownDuration = 1f;
        wallLayerMask = LayerMask.GetMask("Wall"); // set layer mask for walls
    }

    void Update()
{
    if (theDash != null)
    {
        theDash.transform.SetParent(player.transform);
        theDash.name = "lightningDash";
    }

    if (currentCooldown > 0f)
    {
        currentCooldown -= Time.deltaTime;
        animationCooldown -= Time.deltaTime;

        if (animationCooldown <= 0f && theDash != null)
        {
            Destroy(theDash);
        }

        if (currentCooldown >= -0.01f)
        {
            coolDownImage.fillAmount = currentCooldown / cooldownDuration;
        }

        return;
    }

    Vector2 playerPos = player.transform.position;

    if (Input.GetKeyDown(KeyCode.D) && (Time.time - lastPressTime) < doublePressTime)
    {
        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.right, dashLength, wallLayerMask);

        if (hit.collider != null)
        {
            playerPos.x = hit.point.x - 0.1f;
        }
        else
        {
            playerPos.x += dashLength;
        }

        player.transform.position = playerPos;
        theDash = Instantiate(lightningDashPrefab, transform.position + new Vector3(-1.6f, 0), transform.rotation);
        animationCooldown = 0.2f;
        currentCooldown = cooldownDuration;
    }
    else if (Input.GetKeyDown(KeyCode.A) && (Time.time - lastPressTime) < doublePressTime)
    {
        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.left, dashLength, wallLayerMask);

        if (hit.collider != null)
        {
            playerPos.x = hit.point.x + 0.5f;
        }
        else
        {
            playerPos.x -= dashLength;
        }

        player.transform.position = playerPos;
        theDash = Instantiate(lightningDashPrefab, transform.position + new Vector3(1.6f, 0), transform.rotation);
        if (theDash != null)
        {
            Vector3 localScale = theDash.transform.localScale;
            localScale.x *= -1;
            theDash.transform.localScale = localScale;
        }
        animationCooldown = 0.2f;
        currentCooldown = cooldownDuration;
    }
    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
    {
        lastPressTime = Time.time;
    }
}

}
