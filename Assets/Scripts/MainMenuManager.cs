using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("LevelOne");
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Exit");
    }
}
