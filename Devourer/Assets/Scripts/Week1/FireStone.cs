using UnityEngine;

public class FireStone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Breathing>().enabled = true;
            GameObject.Find("MainCanvas/MainUI/SkillPanel/FireSkill").SetActive(true);
            Destroy(gameObject);
        }
    }
}
