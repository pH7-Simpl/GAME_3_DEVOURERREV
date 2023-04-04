using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private Animator animator1;
    private Animator animator2;
    private void Awake() {
        animator1 = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        animator2 = transform.GetChild(1).GetChild(1).GetComponent<Animator>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void quitGame()
    {
        Debug.Log("I Quit");
        Application.Quit();
    }
    public void SetHover(bool x) {
        animator1.SetBool("hover", true);
    }
    public void SetExit(bool x) {
        animator1.SetBool("hover", false);
    }
}
