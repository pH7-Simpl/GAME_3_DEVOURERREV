using UnityEngine;
using UnityEngine.UI;
public class Breathing : MonoBehaviour
{
    private float cooldownDuration;
    private float currentCooldown;
    public GameObject fireBreath;
    private Image coolDownImage;
    PlayerMovement pm;
    void Start()
    {
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/FireSkill/Cooldown").GetComponent<Image>();
        pm = GetComponent<PlayerMovement>();
        cooldownDuration = 3.5f;
        currentCooldown = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentCooldown <= 0f)
        {
            if (pm.isFacingRight)
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
        if(currentCooldown >= -0.01) {
            coolDownImage.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
        }
    }
}
