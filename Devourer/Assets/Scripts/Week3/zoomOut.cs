using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class zoomOut : GoingOutside
{
    public Transform player;
    public float zoomDuration = 5f;
    gameEndMovement gem;
    CreditsLerper cl;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gem = player.GetComponent<gameEndMovement>();
        StartCoroutine(ZoomOutCoroutine());
    }
    private void Update() {
        if (gem.running) {
            gem.horizontal = -1;
            player.transform.localScale = new Vector3(-1f, 1f, 1f);
            gem.isFacingRight = false;
            gem.animator.SetFloat("speed", 1);
        }
    }

    private IEnumerator ZoomOutCoroutine()
    {
        StartScene();
        float elapsedTime = 0f;
        float startOrthoSize = GetComponent<Camera>().orthographicSize;
        float endOrthoSize = GetComponent<Camera>().orthographicSize*5;

        while (elapsedTime < zoomDuration)
        {
            float lerpAmount = elapsedTime / zoomDuration;
            float lerpedOrthoSize = Mathf.Lerp(startOrthoSize, endOrthoSize, lerpAmount);
            GetComponent<Camera>().orthographicSize = lerpedOrthoSize;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GetComponent<Camera>().orthographicSize = endOrthoSize;
        gem.running = false;
        this.enabled = false;
    }
}
