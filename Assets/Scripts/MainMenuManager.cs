using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    bool isGameStarted = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void StartTheGame()
    {
        SceneManager.LoadScene("LevelOne");
        isGameStarted = true;
    }


    public void Exit()
    {
        Application.Quit();
    }
}
