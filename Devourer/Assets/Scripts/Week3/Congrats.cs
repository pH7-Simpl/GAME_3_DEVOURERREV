using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Congrats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] GameObject afterText;
    private bool done = false;
    private void Start()
    {
        StartCoroutine(textFade(endText));
    }
    private void Update() {
        if (done)
        Destroy(gameObject);
    }

    // Update is called once per frame
    
    public IEnumerator fadeIn(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        yield return new WaitForSeconds(2f);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
    private IEnumerator fadeOut(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        yield return new WaitForSeconds(1f);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    private IEnumerator textFade(TextMeshProUGUI i){
        StartCoroutine(fadeIn(5f, endText));
        yield return new WaitForSeconds(8f);
        StartCoroutine(fadeOut(5f, endText));
        done = true;
        if (afterText != null){
            var next = Instantiate(afterText, transform.position, transform.rotation);
            next.transform.SetParent(transform.parent.transform);
        }
    }
}
