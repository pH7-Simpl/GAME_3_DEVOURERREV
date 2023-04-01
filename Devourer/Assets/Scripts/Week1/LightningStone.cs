using UnityEngine;

public class LightningStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<LightningDash>().enabled = true;
            other.gameObject.GetComponent<PlayerMovement>().SetDashUpgrade(true);
            GameObject.Find("MainCanvas/MainUI/SkillPanel/LightningSkill").SetActive(true);
            Destroy(gameObject);
        }
    }
}
