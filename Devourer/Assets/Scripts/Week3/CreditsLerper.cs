using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsLerper : MonoBehaviour
{
    public float displayTime;
    public float slideDuration;
    public RectTransform rectTransform;
    public Vector2 startPos;
    public Vector2 endPos;
    public bool startAtAwake;
    private gameEndMovement gem;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            SlideNotification();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            HideNotification();
        }
    }
    public void ShowNotification()
    {
        Invoke("SlideNotification", 0.1f);
        Invoke("HideNotification", displayTime);
    }

    private void HideNotification()
    {
        StartCoroutine(SlidePanel(endPos, startPos, slideDuration));
    }

    private void SlideNotification()
    {
        StartCoroutine(SlidePanel(startPos, endPos, slideDuration));
    }

    IEnumerator SlidePanel(Vector2 startPosition, Vector2 targetPosition, float duration)
    {
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
        gem = GameObject.FindGameObjectWithTag("Player").GetComponent<gameEndMovement>();
        displayTime = 5f;
        slideDuration = 0.2f;
    }
    private void Update() {
        if(!gem.running && startAtAwake) {
            ShowNotification();
            this.enabled = false;
        }
    }
}
