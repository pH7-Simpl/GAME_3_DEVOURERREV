using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("mainScene");
    }
    public void quitGame()
    {
        Debug.Log("I Quit");
        Application.Quit();
    }
}
