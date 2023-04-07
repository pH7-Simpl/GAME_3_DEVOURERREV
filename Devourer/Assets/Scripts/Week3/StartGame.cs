using UnityEngine;

public class StartGame : GoingOutside
{
    void Awake() {
        Screen.fullScreen = true;
        StartScene();
    }
}
