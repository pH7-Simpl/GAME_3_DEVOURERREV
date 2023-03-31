using UnityEngine;
public class WindStone : MonoBehaviour
{

    public void ShakeScreen()
    {
        ScreenShake screenShake = Camera.main.GetComponent<ScreenShake>();
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
