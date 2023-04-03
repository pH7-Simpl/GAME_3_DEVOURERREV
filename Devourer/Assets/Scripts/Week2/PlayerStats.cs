using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject pointText;
    private int maxPlayerHealth = 100;
    public int playerHealth = 0;
    private float showHBCooldown = 0f;
    private int points;
    private bool showHB = false;
    public void SetShowHB(bool x)
    {
        showHB = x;
    }
    private bool hit = false;
    public void SetHit(bool x)
    {
        hit = x;
    }
    public bool IsHit()
    {
        return hit;
    }

    GameObject healthBar;
    [SerializeField] GameObject MC;
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword")
        {
            playerHealth -= 10;
            showHB = true;
            StartCoroutine(HitEffect(0.5f));
        }
    }

    private void Start()
    {
        maxPlayerHealth = 100;
        showHBCooldown = 0f;
        points = 0;
        MC = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealth = maxPlayerHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
        }
    }

    private void FixedUpdate()
    {
        if (playerHealth <= 0)
        {
            StartCoroutine(Die());
        }
        ShowHealthBar();
    }
    private void ShowHealthBar()
    {
        if (showHB)
        {
            showHBCooldown = 1f;
            healthBar.SetActive(true);
            showHB = false;
        }
        if (showHBCooldown >= 0f)
        {
            showHBCooldown -= Time.deltaTime;
        }
        else
        {
            healthBar.SetActive(false);
        }
    }
    public IEnumerator HitEffect(float duration)
    {
        hit = true;
        yield return new WaitForSeconds(duration);
        hit = false;
    }
    private IEnumerator Die()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        animator.SetBool("died", true);
        yield return new WaitForSeconds(1f);
        MC.transform.SetParent(null);
        FindObjectOfType<gameManager>().SetGameOver(true);
        Destroy(gameObject);
    }
    public IEnumerator PlayerGetPoints(int x)
    {
        points += x;
        float elapsedTime = 0;
        float duration = 0.5f;
        GameObject textPopup = Instantiate(pointText, transform.position, Quaternion.identity);
        textPopup.name = "Text";
        TextMesh text = textPopup.GetComponent<TextMesh>();
        Color oriColor = text.color;
        text.text = "+" + x.ToString();
        Vector3 startPos = textPopup.transform.position;
        while (elapsedTime <= duration)
        {
            float t = elapsedTime / duration;
            textPopup.transform.position = Vector2.Lerp(startPos, startPos + Vector3.up, t);
            text.color = Color.Lerp(oriColor, new Color(oriColor.r, oriColor.g, oriColor.b, 0f), t);
            yield return new WaitForSeconds(Time.deltaTime);
            elapsedTime += Time.deltaTime;
        }
        Destroy(textPopup);
    }

    public void PFRE()
    {
        StartCoroutine(PlayerGetPoints(10));
    }
}
