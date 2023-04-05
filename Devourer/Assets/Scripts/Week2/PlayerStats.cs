using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject pointText;
    private Color originalColor;
    private int maxPlayerHealth = 100;
    public int playerHealth = 0;
    private int points;
    private bool damaged = false;
    private bool hit = false;
    public bool IsHit()
    {
        return hit;
    }

    GameObject healthBar;
    [SerializeField] GameObject MC;
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sword" || other.gameObject.name == "lightningStrike" || other.gameObject.name == "windStrike" || other.gameObject.name == "fireStrike")
        {
            PlayerTakesDamage(0.5f, 5);
        }
    }

    private void Awake()
    {
        maxPlayerHealth = 100;
        points = 0;
        MC = GameObject.FindGameObjectWithTag("MainCamera");
        playerHealth = maxPlayerHealth;
        healthBar = transform.GetChild(1).gameObject;
        healthBar.SetActive(false);
        damaged = false;
        originalColor = GetComponent<SpriteRenderer>().color;
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
    }
    private IEnumerator ShowHealthBar(float duration)
    {
        healthBar.SetActive(true);
        yield return new WaitForSeconds(duration);
        healthBar.SetActive(false);
    }
    public void PlayerTakesDamage(float duration, int damage)
    {
        StartCoroutine(PlayerHit(duration, damage));
    }
    private IEnumerator PlayerHit(float duration, int damage)
    {
        if (!damaged)
        {
            StartCoroutine(colorForDamaged(duration));
            playerHealth -= damage;
            damaged = true;
        }
        damaged = false;
        StartCoroutine(ShowHealthBar(duration));
        hit = true;
        animator.SetBool("hit", hit);
        yield return new WaitForSeconds(duration);
        hit = false;
        animator.SetBool("hit", hit);
    }
    private IEnumerator colorForDamaged(float duration)
    {
        Color targetColor = Color.red;
        float elapsedTime = 0f;
        float t = 0;
        while (elapsedTime <= duration)
        {
            t = elapsedTime / duration;
            GetComponent<SpriteRenderer>().color = Color.Lerp(targetColor, originalColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = originalColor;
    }
    private IEnumerator Die()
    {
        disableThisAndThat();
        animator.SetBool("died", true);
        yield return new WaitForSeconds(1f);
        MC.transform.SetParent(null);
        FindObjectOfType<gameManager>().SetGameOver(true);
        Destroy(gameObject);
    }
    private void disableThisAndThat() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Animator>().SetFloat("speed", 0f);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Slashing>().enabled = false;
        GetComponent<GeyserSeedSpawn>().enabled = false;
        GetComponent<LightningDash>().enabled = false;
        GetComponent<Breathing>().enabled = false;
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

    public void PFDE()
    {
        StartCoroutine(PlayerGetPoints(10));
    }
    public void PFDB()
    {
        StartCoroutine(PlayerGetPoints(100));
    }
}
