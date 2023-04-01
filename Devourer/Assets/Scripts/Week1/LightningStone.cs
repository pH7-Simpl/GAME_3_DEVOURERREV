using UnityEngine;

public class LightningStone : MonoBehaviour
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
            other.gameObject.GetComponent<LightningDash>().enabled = true;
            other.gameObject.GetComponent<PlayerMovement>().SetDashUpgrade(true);
            GameObject.Find("MainCanvas/MainUI/SkillPanel/LightningSkill").SetActive(true);
            Destroy(gameObject);
        }
    }
}
