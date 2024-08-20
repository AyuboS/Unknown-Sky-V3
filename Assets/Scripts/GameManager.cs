using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private GameObject tutorialAnim; // The GameObject containing the tutorial animation

    // List of scenes where the tutorial should be shown (you can also use build indices)
    [SerializeField] private string[] tutorialScenes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Early return to prevent running the rest of the code on the duplicate.
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the correct tutorialAnim for the current scene
        FindAndAssignTutorialAnim();

        // Show the tutorial only if the current scene is in the list of tutorial scenes
        if (ShouldShowTutorial(scene.name))
        {
            ShowTutorial();
        }

        // Reassign any references to the GameManager in other objects here
        var gameOverManagers = FindObjectsOfType<GameOverManager>();
        foreach (var manager in gameOverManagers)
        {
            manager.ReassignGameManager(this);
        }

        // Also reassign the transition animator as needed
        transitionAnim = FindObjectOfType<Animator>();
    }

    private void FindAndAssignTutorialAnim()
    {
        // Find the tutorialAnim object in the current scene
        tutorialAnim = GameObject.FindWithTag("TutorialText");

        if (tutorialAnim != null)
        {
            tutorialAnim.SetActive(false); // Initially disable the tutorial UI
        }
        else
        {
            Debug.LogWarning("No TutorialText object found in the current scene.");
        }
    }

    public void ReassignGameManager(GameManager newManager)
    {
        Instance = newManager;
    }

    public void NextLevel()
    {
        if (transitionAnim != null)
        {
            StartCoroutine(LoadNextLevel());
        }
        else
        {
            Debug.LogWarning("Transition Animator is not assigned!");
        }
    }

    IEnumerator LoadNextLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }

    public void DeathTrigger()
    {
        if (transitionAnim != null)
        {
            transitionAnim.SetTrigger("End");
        }
        else
        {
            Debug.LogWarning("Transition Animator is not assigned!");
        }
    }

    // Method to check if the tutorial should be shown in the current scene
    private bool ShouldShowTutorial(string sceneName)
    {
        foreach (var tutorialScene in tutorialScenes)
        {
            if (sceneName == tutorialScene)
            {
                return true;
            }
        }
        return false;
    }

    // Method to show the tutorial UI with an animation
    public void ShowTutorial()
    {
        if (tutorialAnim != null)
        {
            tutorialAnim.SetActive(true); // Enable the tutorial UI

            // Start the animation using the Animator component on the tutorial UI
            Animator tutorialAnimator = tutorialAnim.GetComponent<Animator>();
            if (tutorialAnimator != null)
            {
                tutorialAnimator.SetTrigger("Start");

                // Optionally, disable the tutorial UI after the animation ends
                StartCoroutine(HideTutorialAfterAnimation(tutorialAnimator));
            }
            else
            {
                Debug.LogWarning("No Animator component found on the tutorialAnim GameObject.");
            }
        }
    }

    // Coroutine to hide the tutorial UI after the animation finishes
    private IEnumerator HideTutorialAfterAnimation(Animator tutorialAnimator)
    {
        // Wait for the length of the animation clip (assuming it's the first clip in the Animator)
        AnimatorClipInfo[] clipInfo = tutorialAnimator.GetCurrentAnimatorClipInfo(0);
        float animationLength = clipInfo[0].clip.length;

        yield return new WaitForSeconds(animationLength);

        tutorialAnim.SetActive(false); // Disable the tutorial UI after the animation ends
    }
}
