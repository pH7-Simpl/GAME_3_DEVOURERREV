using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private bool seeMap;
    public void SetSeeMap(bool x)
    {
        seeMap = x;
    }
    public bool IsSeeMap()
    {
        return seeMap;
    }
    [SerializeField] private bool gameOver;
    public bool GetGameOver() {
        return gameOver;
    }
    public void SetGameOver(bool x)
    {
        gameOver = x;
    }
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Soldier;
    [SerializeField] private GameObject Drone;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Camera MC;
    PlayerStats ps;
    [SerializeField] private bool doorOpening;
    public void SetDoorOpening(bool x)
    {
        doorOpening = x;
    }
    public void Awake()
    {
        // Instantiate(Drone, new Vector3(-120f, 40f, 0f), Quaternion.identity);
        Instantiate(Drone, new Vector3(-110f, 40f, 0f), Quaternion.identity);
        // Instantiate(Soldier, new Vector3(-110f, 0f, 0f), Quaternion.identity);
        // Instantiate(Soldier, new Vector3(-100f, 0f, 0f), Quaternion.identity);
        Instantiate(Soldier, new Vector3(-90f, 0f, 0f), Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player");
        ps = FindObjectOfType<PlayerStats>();
        MC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1").SetActive(true);
        gameOver = false;
        paused = false;
        seeMap = false;
        doorOpening = false;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
            }
            if (paused)
            {
                PauseCommand();
            }
            else
            {
                UnpauseCommand();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                seeMap = !seeMap;
            }
            if (!paused)
            {
                if (seeMap)
                {
                    SeeMap();
                }
                else
                {
                    UnseeMap();
                }
            }
        }
        else
        {
            doorOpening = false;
            seeMap = false;
            paused = false;
            pauseUI.SetActive(false);
            mainUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }
    public void Continue()
    {
        if (paused)
        {
            paused = false;
            UnpauseCommand();
        }
    }
    private void PauseCommand()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        mainUI.SetActive(false);
    }
    private void UnpauseCommand()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        mainUI.SetActive(true);
    }
    private void SeeMap()
    {
        if (!doorOpening)
        {
            Time.timeScale = 0f;
            // mainUI.SetActive(false);
            // if (MC != null)
            // {
                // MC.transform.SetParent(null);
                // MC.transform.position = new Vector3(0f, 0f, -5f);
                // MC.orthographicSize = 50f;
            // }
        }
    }
    private void UnseeMap()
    {
        if (!doorOpening)
        {
            Time.timeScale = 1f;
            // mainUI.SetActive(true);
            // if (MC != null && player != null)
            // {
                // MC.transform.SetParent(player.transform);
                // MC.orthographicSize = 5;
            // }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Debug.Log("Going to Main Menu");
    }
}
