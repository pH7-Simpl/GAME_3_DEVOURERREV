using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private bool seeMap;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private Camera MC;
    PlayerStats ps;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = FindObjectOfType<PlayerStats>(); 
        MC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
        GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1").SetActive(true);
        paused = false;
        seeMap = false;
    }

    private void Update()
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
        if(!paused && ps.playerHealth > 0) {
            Time.timeScale = 0f;
            mainUI.SetActive(false);
            if(MC != null) {
                MC.transform.SetParent(null);
                MC.transform.position = new Vector3(0f, 0f, -5f);
                MC.orthographicSize = 100f;
            }
        }
    }
    private void UnseeMap() {
        if(!paused && ps.playerHealth > 0) {
            Time.timeScale = 1f;
            mainUI.SetActive(true);
            MC.orthographicSize = 5;
            if(player != null) {
                MC.transform.SetParent(player.transform);
            }
        }
    }
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        Debug.Log("Going to Main Menu");
    }
}
