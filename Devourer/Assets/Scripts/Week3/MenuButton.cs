using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
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
}
