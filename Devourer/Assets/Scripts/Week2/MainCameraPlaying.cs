using UnityEngine;

public class MainCameraPlaying : MonoBehaviour
{
    private float shakeDuration = 0.1f;
    public void SetShakeDuration(float sd) {
        shakeDuration = sd;
    } 
    private float shakeAmount = 0.2f;
    private float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private PlayerStats playerStats;
    private void Start()
    {
        originalPos = transform.localPosition;
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats.playerHealth <= 0)
        {
            shakeDuration = 0f;
        }
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
