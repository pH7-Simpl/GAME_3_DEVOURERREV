using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    public float hoverHeight;
    public float hoverTime;

    private Vector3 startPos;
    private Vector3 endPos;
    private float timer;

    private void Start()
    {
        hoverHeight = 0.2f;
        hoverTime = 0.5f;
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + hoverHeight, startPos.z);
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / hoverTime, 1f);
        transform.position = Vector3.Lerp(startPos, endPos, t);
    }
}
