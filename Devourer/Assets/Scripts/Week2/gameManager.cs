using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainUI;
    void Start()
    {
        GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1").SetActive(true);
        paused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
        }
        if(paused) {
            PauseCommand();
        } else {
            UnpauseCommand();
        }
    }
    public void Continue() {
        if(paused) {
            paused = false;
            UnpauseCommand();
        }
    }
    private void PauseCommand() {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        mainUI.SetActive(false);
    }
    private void UnpauseCommand() {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        mainUI.SetActive(true);
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        Debug.Log("Going to Main Menu");
    }
}
