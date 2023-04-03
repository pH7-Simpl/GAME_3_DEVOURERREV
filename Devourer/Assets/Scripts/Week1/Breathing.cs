using UnityEngine;
using UnityEngine.UI;
public class Breathing : MonoBehaviour
{
    private float cooldownDuration;
    private float currentCooldown;
    [SerializeField] private GameObject fireBreath;
    private Image coolDownImage;
    private PlayerMovement pm;
    void Start()
    {
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/FireSkill/Cooldown").GetComponent<Image>();
        pm = GetComponent<PlayerMovement>();
        cooldownDuration = 3.5f;
        currentCooldown = 0f;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && currentCooldown <= 0f)
            {
                if (pm.IsFacingRight())
                {
                    Instantiate(fireBreath, transform.position + new Vector3(1.5f, 0.1f), transform.rotation);
                }
                else
                {
                    Instantiate(fireBreath, transform.position + new Vector3(-1.5f, 0.1f), transform.rotation);
                }
                currentCooldown = cooldownDuration;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown >= -0.01)
            {
                coolDownImage.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
            }
        }
    }
}
