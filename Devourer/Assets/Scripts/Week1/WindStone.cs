using UnityEngine;
public class WindStone : MonoBehaviour
{

    public void ShakeScreen()
    {
        MainCameraPlaying screenShake = Camera.main.GetComponent<MainCameraPlaying>();
        if (screenShake != null)
        {
            screenShake.shakeDuration = 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShakeScreen();
            other.gameObject.GetComponent<Slashing>().canDownSlash = true;
            GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill2").SetActive(true);
            Destroy(gameObject);
            other = null;
        }
    }
}
