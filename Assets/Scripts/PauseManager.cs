using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject pauseText;
    [SerializeField] GameObject aimObject;

    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1.0f;  // Ensure time scale is reset when the game starts
        isPaused = false;       // Reset the pause state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseButton();
            }
        }
    }

    public void PauseButton()
    {
        aimObject.SetActive(false);
        pauseText.SetActive(false);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        aimObject.SetActive(true);
        pauseText.SetActive(true);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void toMainMenu()
    {
        Time.timeScale = 1.0f; // Reset time scale before returning to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}
