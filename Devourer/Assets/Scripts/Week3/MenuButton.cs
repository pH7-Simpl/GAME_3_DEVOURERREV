using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject MainUI;
    [SerializeField] private GameObject EEUI;
    private Animator animator;
    private TMPro.TextMeshProUGUI text;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
    public void SetHover(int y) {
        text = transform.GetChild(y).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        animator = transform.GetChild(y).GetChild(1).GetComponent<Animator>();
        text.fontStyle = TMPro.FontStyles.Italic;
        animator.SetBool("hover", true);
    }
    public void SetExit(int y) {
        text = transform.GetChild(y).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        animator = transform.GetChild(y).GetChild(1).GetComponent<Animator>();
        text.fontStyle = TMPro.FontStyles.Normal;
        animator.SetBool("hover", false);
    }
    public void switchBetWeen(bool x) {
        MainUI.SetActive(x);
        EEUI.SetActive(!x);
    }
}
