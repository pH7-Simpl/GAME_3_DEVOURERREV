using UnityEngine;
using UnityEngine.UI;
public class GeyserSeedSpawn : MonoBehaviour
{
    private float cooldownDuration;
    private float currentCooldown;
    public GameObject seed;
    private Image coolDownImage;
    PlayerMovement pm;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/WaterSkill/Cooldown").GetComponent<Image>();
        currentCooldown = 0f;
        cooldownDuration = 3f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentCooldown <= 0f)
        {
            if (pm.IsFacingRight())
            {
                Instantiate(seed, transform.position + new Vector3(2.5f, 3f), transform.rotation);
            }
            else
            {
                Instantiate(seed, transform.position + new Vector3(-2.5f, 3f), transform.rotation);
            }
            currentCooldown = cooldownDuration;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
        if(currentCooldown >= -0.01f) {
            coolDownImage.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
        } 
    }
}
