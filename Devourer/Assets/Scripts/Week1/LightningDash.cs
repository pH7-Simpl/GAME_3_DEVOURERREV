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
    public GameObject theDash = null;
    private PlayerMovement pm;
    private int wallLayerMask;
    private bool canSkill;
    private Vector3 playerPos;
    public void SetCanSkill(bool x) {
        canSkill = x;
    }

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/LightningSkill/Cooldown").GetComponent<Image>();
        doublePressTime = 0.2f;
        dashLength = 6f;
        currentCooldown = 0f;
        animationCooldown = 0f;
        cooldownDuration = 1f;
        wallLayerMask = LayerMask.GetMask("Wall");
        canSkill = true;
    }

    private void Update()
    {
        if (Time.timeScale != 0 && canSkill)
        {
            if (theDash != null)
            {
                theDash.name = "LightningDash";
            }

            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.deltaTime;
                animationCooldown -= Time.deltaTime;

                if (animationCooldown <= 0f && theDash != null)
                {
                    Destroy(theDash);
                }

                if (currentCooldown >= -0.5f)
                {
                    coolDownImage.fillAmount = currentCooldown / cooldownDuration;
                }

                return;
            }
            if (Input.GetKeyDown(KeyCode.D) && (Time.time - lastPressTime) < doublePressTime)
            {
                playerPos = GetComponent<Transform>().position;
                RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.right, dashLength, wallLayerMask);

                if (hit.collider != null)
                {
                    playerPos.x = hit.point.x - 0.1f;
                }
                else
                {
                    playerPos.x += dashLength;
                }
                instansiasu();
            }
            else if (Input.GetKeyDown(KeyCode.A) && (Time.time - lastPressTime) < doublePressTime)
            {
                playerPos = GetComponent<Transform>().position;
                RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.left, dashLength, wallLayerMask);

                if (hit.collider != null)
                {
                    playerPos.x = hit.point.x + 0.5f;
                }
                else
                {
                    playerPos.x -= dashLength;
                }
                instansiasu();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                lastPressTime = Time.time;
            }
        }
    }
    private void instansiasu() {
        transform.position = playerPos;
        theDash = Instantiate(lightningDashPrefab, transform.position + new Vector3(transform.localScale.x*-1.6f, 0.01f), transform.rotation, transform);
        animationCooldown = 0.2f;
        currentCooldown = cooldownDuration;
    }

}
