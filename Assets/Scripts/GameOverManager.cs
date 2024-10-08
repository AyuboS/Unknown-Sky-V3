using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    // This is the method that allows reassignment of the GameManager instance
    public void ReassignGameManager(GameManager newManager)
    {
        gameManager = newManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.DeathTrigger();
                StartCoroutine(ToMainMenu());
            }
            else
            {
                Debug.LogWarning("GameManager is null or has been destroyed.");
            }
        }
    }

    IEnumerator ToMainMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
