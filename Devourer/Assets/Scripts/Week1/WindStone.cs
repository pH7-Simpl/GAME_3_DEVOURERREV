using UnityEngine;
public class WindStone : MonoBehaviour
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
            other.gameObject.GetComponent<Slashing>().SetCanDownSlash(true);
            GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill2").SetActive(true);
            GameObject keterangan =  GameObject.Find("MainCanvas/MainUI/SkillAcquired");
            keterangan.SetActive(true);
            keterangan.GetComponent<SkillAcquiredNotification>().ShowNotification("DOWN SLASH", "Shift + Space");
            Destroy(gameObject);
            other = null;
        }
    }
}
