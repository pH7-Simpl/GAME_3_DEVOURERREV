using UnityEngine;

public class WaterStone : MonoBehaviour
{
    private void ShakeScreen()
    {
        MainCameraPlaying screenShake = Camera.main.GetComponent<MainCameraPlaying>();
        if (screenShake != null)
        {
            screenShake.SetShakeDuration(0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShakeScreen();
            other.gameObject.GetComponent<GeyserSeedSpawn>().enabled = true;
            GameObject.Find("MainCanvas/MainUI/SkillPanel/WaterSkill").SetActive(true);
            GameObject keterangan =  GameObject.Find("MainCanvas/MainUI/SkillAcquired");
            keterangan.SetActive(true);
            keterangan.GetComponent<SkillAcquiredNotification>().ShowNotification("WATER SPROUT", "Q");
            Destroy(gameObject);
        }
    }
}
