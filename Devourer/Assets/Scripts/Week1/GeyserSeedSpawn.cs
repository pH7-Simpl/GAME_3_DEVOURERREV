using UnityEngine;
using UnityEngine.UI;
public class GeyserSeedSpawn : MonoBehaviour
{
    private int wallLayerMask;
    private Vector3 spawnRange;
    private float spawnLength;
    private float cooldownDuration;
    private float currentCooldown;
    [SerializeField] private GameObject seed;
    private Image coolDownImage;
    private PlayerMovement pm;
    private bool canSkill;
    public void SetCanSkill(bool x) {
        canSkill = x;
    }
    private void Start()
    {
        wallLayerMask = LayerMask.GetMask("Wall");
        pm = GetComponent<PlayerMovement>();
        coolDownImage = GameObject.Find("MainCanvas/MainUI/SkillPanel/WaterSkill/Cooldown").GetComponent<Image>();
        currentCooldown = 0f;
        cooldownDuration = 3f;
        canSkill = true;
        spawnLength = 2.5f;
    }
    private void Update()
    {
        Vector2 playerPos = transform.position;
        if (Time.timeScale != 0 && canSkill)
        {
            if (Input.GetKeyDown(KeyCode.Q) && currentCooldown <= 0f)
            {
                if(transform.localScale.x == 1) {
                    spawnRange = transform.position + new Vector3(spawnLength, 0.5f);
                    RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.right, spawnLength, wallLayerMask);
                    if(hit.collider != null) {
                        spawnRange.x = hit.point.x - 0.5f;
                    }
                } else {
                    spawnRange = transform.position + new Vector3(-spawnLength, 0.5f);
                    RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.left, spawnLength, wallLayerMask);
                    if(hit.collider != null) {
                        spawnRange.x = hit.point.x + 0.5f;
                    }
                }
                Instantiate(seed, spawnRange, transform.rotation);
                currentCooldown = cooldownDuration;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown >= -0.01f)
            {
                coolDownImage.fillAmount = Mathf.Clamp01(currentCooldown / cooldownDuration);
            }
        }
    }
}
