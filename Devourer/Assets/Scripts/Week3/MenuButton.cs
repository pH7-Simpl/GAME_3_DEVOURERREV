using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject MainUI;
    [SerializeField] private GameObject EEUI;
    private Animator animator;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void quitGame()
    {
        Debug.Log("I Quit");
        Application.Quit();
    }
    public void SetHover(int y) {
        animator = transform.GetChild(y).GetChild(1).GetComponent<Animator>();
        animator.SetBool("hover", true);
    }
    public void SetExit(int y) {
        animator = transform.GetChild(y).GetChild(1).GetComponent<Animator>();
        animator.SetBool("hover", false);
    }
    public void switchBetWeen(bool x) {
        MainUI.SetActive(x);
        EEUI.SetActive(!x);
    }
}
