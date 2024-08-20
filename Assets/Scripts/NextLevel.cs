using System.Collections;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    // This method allows reassignment of the GameManager instance
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
                gameManager.NextLevel();
            }
            else
            {
                Debug.LogWarning("GameManager is null or has been destroyed.");
            }
        }
    }
}
