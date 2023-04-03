using UnityEngine;
using UnityEngine.UI;

public class SkillAcquiredNotification : MonoBehaviour
{
    public float displayTime = 2.0f;
    
    public void ShowNotification(string skillName, string skillButton)
    {
        transform.GetChild(2).GetComponent<Text>().text = "New skill acquired!\n\n\n\nTo activate this skill press " + skillButton;
        transform.GetChild(3).GetComponent<Text>().text = skillName;
        Invoke("HideNotification", displayTime);
    }
    
    void HideNotification()
    {
        gameObject.SetActive(false);
    }
}
