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
            GameObject.Find("MainCanvas/MainUI/SkillAcquired").GetComponent<SkillAcquiredNotification>().ShowNotification("DOWN SLASH", "Shift + Space");
            Destroy(gameObject);
            other = null;
        }
    }
}
