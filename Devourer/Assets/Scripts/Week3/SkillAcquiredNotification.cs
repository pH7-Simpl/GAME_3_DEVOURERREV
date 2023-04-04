using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillAcquiredNotification : MonoBehaviour
{
    public float displayTime;
    public float slideDuration;
    public float slideDistance;
    private RectTransform rectTransform;

    public void ShowNotification(string skillName, string skillButton)
    {
        transform.GetChild(2).GetComponent<Text>().text = "New skill acquired!\n\n\n\nTo activate this skill press " + skillButton;
        transform.GetChild(3).GetComponent<Text>().text = skillName;
        Invoke("SlideNotification", 0.1f);
        Invoke("HideNotification", displayTime);
    }

    private void HideNotification()
    {
        StartCoroutine(SlidePanel(slideDistance, slideDuration));
    }

    private void SlideNotification()
    {
        StartCoroutine(SlidePanel(-slideDistance, slideDuration));
    }

    IEnumerator SlidePanel(float targetX, float duration)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = new Vector2(targetX, startPosition.y);

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        displayTime = 2f;
        slideDuration = 0.2f;
        slideDistance = 484f;
    }
}
