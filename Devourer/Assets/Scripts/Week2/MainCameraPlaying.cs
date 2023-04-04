using UnityEngine;

public class MainCameraPlaying : MonoBehaviour
{
    private float shakeDuration;
    public void SetShakeDuration(float sd)
    {
        shakeDuration = sd;
    }
    private float shakeAmount = 0.2f;
    private float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private PlayerStats playerStats;
    private void Awake()
    {
        shakeAmount = 0.2f;
        decreaseFactor = 1.0f;
        shakeDuration = 0f;
        originalPos = transform.localPosition;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
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
            if (playerStats.playerHealth > 0)
            {
                transform.localPosition = originalPos;
            }
        }
    }


}
