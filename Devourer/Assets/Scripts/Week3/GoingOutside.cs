using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GoingOutside : MonoBehaviour
{
    public Image fadeImage;
    public Color oriColor;
    private void Awake() {
        oriColor = fadeImage.color;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(FadeIn(fadeImage, 2f, () => SceneManager.LoadScene(index)));
    }
    public void StartScene()
    {
        StartCoroutine(FadeOut(fadeImage, 2f));
    }

    private IEnumerator FadeOut(Image image, float duration)
    {
        image.gameObject.SetActive(true);
        float startTime = Time.time;
        float startAlpha = image.color.a;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color color = image.color;
            color.a = Mathf.Lerp(startAlpha, 0f, t);
            image.color = color;
            yield return null;
        }
        image.gameObject.SetActive(false);
    }
    private IEnumerator FadeIn(Image image, float duration, System.Action onComplete = null)
    {
        float startTime = Time.time;
        Color oriColor = image.color;
        float startAlpha = image.color.a;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            Color color = oriColor;
            color.a = Mathf.Lerp(startAlpha, 1f, t);
            image.color = color;
            yield return null;
        }
        if (onComplete != null)
        {
            onComplete();
        }
    }
    private void OnSceneUnloaded(Scene scene)
    {
        fadeImage.gameObject.SetActive(false);
        fadeImage.color = oriColor;
    }
}
