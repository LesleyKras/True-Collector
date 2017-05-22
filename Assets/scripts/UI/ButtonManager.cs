using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public void StartGameBtn(string NewGameLevel) {
        SceneManager.LoadScene(NewGameLevel);
    }

    public void ExitGameBtn() {
        Application.Quit();
    }

    public void ControlBtn(string controls) {
        SceneManager.LoadScene(controls);
    }

    public void BackBtn(string home) {
        SceneManager.LoadScene(home);
    }
}
