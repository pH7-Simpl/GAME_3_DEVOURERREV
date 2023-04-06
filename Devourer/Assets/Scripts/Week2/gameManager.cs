using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private bool paused;
    public bool IsPaused()
    {
        return paused;
    }
    [SerializeField] private GameObject MapCamObj;
    [SerializeField] private GameObject playerMarkerObj;
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
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject gameOverUI; 
    private Camera MC;
    private Camera MapCam;
    private PlayerStats ps;
    private bool skillTaken;
    public void setSkillTaken(bool x) {
        skillTaken = x;
    }
    private GameObject playerMarker;
    private bool doorOpening;
    public void SetDoorOpening(bool x)
    {
        doorOpening = x;
    }
    public bool GetDoorOpening()
    {
        return doorOpening;
    }
    private bool lift1;
    public void SetLift1(bool x)
    {
        lift1 = x;
    }
    private bool lift2;
    public void SetLift2(bool x)
    {
        lift2 = x;
    }
    private gameEndMovement gem;
    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMarker = Instantiate(playerMarkerObj, player.transform.position, Quaternion.identity);
        ps = FindObjectOfType<PlayerStats>();
        MC = Camera.main;
        GameObject.Find("MainCanvas/MainUI/SkillPanel/AirSkill1").SetActive(true);
        gameOver = false;
        paused = false;
        seeMap = false;
        doorOpening = false;
        MapCam = Instantiate(MapCamObj, new Vector3(-1f, 7f, -5f), Quaternion.identity).GetComponent<Camera>();
        MapCam.orthographicSize = 30f;
        MapCam.transform.SetParent(transform);
        MC.enabled = true;
        MapCam.enabled = false; 
        skillTaken = false;
        gem = player.GetComponent<gameEndMovement>();
    }

    void Update()
    {
        if(player != null) {
            if(!doorOpening) {
                playerMarker.transform.position = player.transform.position;
            }
        }
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
            if (Input.GetKeyDown(KeyCode.M) && !doorOpening && !skillTaken && !lift1 && !lift2)
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
                MapCam.enabled = seeMap;
                MC.enabled = !seeMap;
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
            mainUI.SetActive(false);
        }
    }
    private void UnseeMap()
    {
        if (!doorOpening)
        {
            Time.timeScale = 1f;
            mainUI.SetActive(true);
        }
    }
    public void MainMenu()
    {
        gameOver = false;
        if(paused) {
            paused = false;
        }
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        gameOver = false;
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
    
}
