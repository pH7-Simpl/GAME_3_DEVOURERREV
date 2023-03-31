using UnityEngine;

public class WaterStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<GeyserSeedSpawn>().enabled = true;
            GameObject.Find("MainCanvas/MainUI/SkillPanel/WaterSkill").SetActive(true);
            Destroy(gameObject);
        }
    }
}
