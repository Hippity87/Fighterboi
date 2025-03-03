using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; } // Singleton instance
    private Canvas mainMenuCanvas;    // Will be found in MainMenu scene
    private Canvas pauseMenuCanvas;   // Will be found in Game scene
    private bool isPaused = false;

    void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    void Start()
    {
        // Force initial load to MainMenu if not already
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Starting in scene: " + currentSceneIndex); // Debug startup scene
        if (currentSceneIndex != 0)
        {
            SceneManager.LoadScene("MainMenu"); // Redirect to scene 0 on startup
            return;
        }

        // Find and activate canvas based on current scene
        if (currentSceneIndex == 0) // MainMenu scene
        {
            mainMenuCanvas = GameObject.Find("MainMenuCanvas")?.GetComponent<Canvas>();
            Debug.Log("MainMenuCanvas found: " + (mainMenuCanvas != null)); // Debug canvas
            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.gameObject.SetActive(true); // Explicitly activate
                Debug.Log("MainMenuCanvas activated: " + mainMenuCanvas.gameObject.activeSelf); // Debug activation
                GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject fighter in fighters)
                {
                    if (fighter.scene == SceneManager.GetActiveScene())
                    {
                        fighter.SetActive(false);
                        Debug.Log("Disabled fighter: " + fighter.name); // Debug fighter state
                    }
                }
            }
            else
            {
                Debug.LogError("MainMenuCanvas not found in scene 0!");
            }
        }
        else if (currentSceneIndex == 1) // Game scene
        {
            pauseMenuCanvas = GameObject.Find("PauseMenuCanvas")?.GetComponent<Canvas>();
            Debug.Log("PauseMenuCanvas found: " + (pauseMenuCanvas != null)); // Debug canvas
            GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject fighter in fighters)
            {
                if (fighter.scene == SceneManager.GetActiveScene())
                {
                    fighter.SetActive(true);
                    Debug.Log("Enabled fighter: " + fighter.name); // Debug fighter state
                }
            }
        }
        Time.timeScale = 1; // Ensure game runs normally
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1 && Input.GetKeyDown(KeyCode.Escape)) // Only pause in Game scene
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Loads scene 1
        Debug.Log("Starting Game scene"); // Debug transition
    }

    public void ShowOptions()
    {
        Debug.Log("Options menu opened (functionality TBD)");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop in Editor
#else
        Application.Quit(); // Quit built game
#endif
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1) // Only toggle pause in Game scene
        {
            isPaused = !isPaused;
            if (pauseMenuCanvas != null) pauseMenuCanvas.gameObject.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1; // Freeze/unfreeze time
            Debug.Log("TogglePause: " + (isPaused ? "Paused" : "Resumed")); // Debug pause
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // Returns to scene 0
        Debug.Log("Exiting to MainMenu"); // Debug transition
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null; // Clean up singleton on destroy
    }
}