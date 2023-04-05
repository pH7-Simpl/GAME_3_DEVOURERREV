using UnityEngine;
using UnityEngine.UI;
public class Slashing : MonoBehaviour
{
    [SerializeField] private float cooldownDuration;
    [SerializeField] private GameObject windSlashPrefab;
    [SerializeField] private GameObject downSlashPrefab;
    private Image coolDownImage1;
    private Image coolDownImage2;

    private float currentCooldown;
    private bool canDownSlash = false;
    private bool canSkill;
    public void SetCanSkill(bool x) {
        canSkill = x;
    }
    public void SetCanDownSlash(bool x)
    {
        canDownSlash = x;
    }
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        coolDownImage1 = GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1/Cooldown").GetComponent<Image>();
        coolDownImage2 = GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill2/Cooldown").GetComponent<Image>();
        cooldownDuration = 0.75f;
        currentCooldown = 0f;
        canSkill = true;
    }

    private void Update()
    {
        if (Time.timeScale != 0 && canSkill)
        {
            if (!playerMovement.IsGrounded() && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space) && currentCooldown <= 0f)
            {
                if (canDownSlash)
                {
                    Instantiate(downSlashPrefab, transform.position + Vector3.down, Quaternion.identity);
                    currentCooldown = cooldownDuration;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F) && currentCooldown <= 0f)
            {
                Vector3 slashPosition = playerMovement.IsFacingRight() ? Vector3.right : Vector3.left;
                Instantiate(windSlashPrefab, transform.position + slashPosition + new Vector3(0, 0.01f), Quaternion.identity);
                currentCooldown = cooldownDuration;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown >= -0.01f)
            {
                coolDownImage1.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
                coolDownImage2.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
            }
        }
    }
}
