using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private bool seeMap;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private Camera MC;
    void Start()
    {
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();  
        GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1").SetActive(true);
        paused = false;
        seeMap = false;
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
        if(Input.GetKeyDown(KeyCode.M)) {
            seeMap = !seeMap;
        }
        if(seeMap) {
            SeeMap();
        } else {
            UnseeMap();
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
    private void SeeMap() {
        if(!paused) {
            Time.timeScale = 0f;
            mainUI.SetActive(false);
            MC.transform.SetParent(null);
            MC.orthographicSize = 100f;
        }
    }
    private void UnseeMap() {
        if(!paused) {
            Time.timeScale = 1f;
            mainUI.SetActive(true);
            MC.transform.SetParent(GameObject.Find("Player").transform);
            MC.orthographicSize = 5;
        }
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        Debug.Log("Going to Main Menu");
    }
}
