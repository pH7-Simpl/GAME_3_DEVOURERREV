using UnityEngine;

public class FireStone : MonoBehaviour
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
            other.gameObject.GetComponent<Breathing>().enabled = true;
            GameObject.Find("MainCanvas/MainUI/SkillPanel/FireSkill").SetActive(true);
            GameObject keterangan =  GameObject.Find("MainCanvas/MainUI/SkillAcquired");
            keterangan.SetActive(true);
            keterangan.GetComponent<SkillAcquiredNotification>().ShowNotification("FIRE BREATH", "E");
            Destroy(gameObject);
        }
    }
}
