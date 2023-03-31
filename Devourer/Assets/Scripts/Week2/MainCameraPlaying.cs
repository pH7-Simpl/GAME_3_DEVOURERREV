using UnityEngine;

public class MainCameraPlaying : MonoBehaviour
{
    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.2f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalPos;
        }
    }

}
