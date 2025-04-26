using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; } // Singleton instance
    public static bool IsPaused { get; private set; } // Global pause state
    private Canvas mainMenuCanvas;    // Will be found in MainMenu scene
    private Canvas pauseMenuCanvas;   // Will be found in Game scene
    private bool hasAssignedButtons = false; // Flag to ensure button assignment runs once

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
        IsPaused = false; // Reset pause state on Awake
        Debug.Log("MenuManager Awake: IsPaused reset to " + IsPaused);
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
                Debug.Log("Found " + fighters.Length + " fighters with tag 'Player' in scene 0"); // Debug tag count
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
            IsPaused = false; // Ensure game starts unpaused
            Time.timeScale = 1; // Ensure time is running
            Debug.Log("Game scene loaded: IsPaused = " + IsPaused + ", Time.timeScale = " + Time.timeScale);

            // Debug all GameObjects in the scene
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            Debug.Log("Total GameObjects in scene: " + allObjects.Length);
            foreach (GameObject obj in allObjects)
            {
                if (obj.scene == SceneManager.GetActiveScene())
                {
                    Debug.Log("GameObject: " + obj.name + " at path: " + GetFullPath(obj));
                }
            }

            // Find PauseMenuCanvas
            Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            Debug.Log("Total Canvases found: " + canvases.Length);
            foreach (Canvas canvas in canvases)
            {
                Debug.Log("Found canvas: " + canvas.name + " at path: " + GetFullPath(canvas.gameObject));
                if (canvas.name == "PauseMenuCanvas")
                {
                    pauseMenuCanvas = canvas;
                    Debug.Log("Assigned PauseMenuCanvas in Start");
                    break;
                }
            }

            Debug.Log("PauseMenuCanvas found: " + (pauseMenuCanvas != null));
            if (pauseMenuCanvas != null)
            {
                // Ensure canvas state matches IsPaused
                pauseMenuCanvas.gameObject.SetActive(IsPaused); // Should be false on start
                Debug.Log("PauseMenuCanvas initial state set to: " + pauseMenuCanvas.gameObject.activeSelf);
            }
            else
            {
                Debug.LogError("PauseMenuCanvas not found in Game scene during Start!");
            }

            GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log("Found " + fighters.Length + " fighters with tag 'Player' in scene 1"); // Debug tag count
            foreach (GameObject fighter in fighters)
            {
                if (fighter.scene == SceneManager.GetActiveScene())
                {
                    fighter.SetActive(true);
                    Debug.Log("Enabled fighter: " + fighter.name); // Debug fighter state
                }
            }
        }
    }

    void LateUpdate()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1 && pauseMenuCanvas != null && !hasAssignedButtons)
        {
            // Dynamically assign button events
            Button[] buttons = pauseMenuCanvas.GetComponentsInChildren<Button>(true); // Include inactive objects
            Debug.Log("Total buttons found under PauseMenuCanvas: " + buttons.Length);
            foreach (Button button in buttons)
            {
                Debug.Log("Found button: " + button.name + " at path: " + GetFullPath(button.gameObject));
                if (button.name == "ResumeButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ResumeGame);
                    Debug.Log("ResumeButton OnClick assigned to ResumeGame");
                }
                else if (button.name == "OptionsButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ShowOptions);
                    Debug.Log("OptionsButton OnClick assigned to ShowOptions");
                }
                else if (button.name == "ExitToMainMenuButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ExitToMainMenu);
                    Debug.Log("ExitToMainMenuButton OnClick assigned to ExitToMainMenu");
                }
            }
            hasAssignedButtons = true; // Prevent reassigning every frame
        }
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1 && Input.GetKeyDown(KeyCode.F1)) // Toggle pause with F1 in Game scene
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Loads scene 1
        Debug.Log("Starting Game scene"); // Debug transition
        hasAssignedButtons = false; // Reset for next Game scene load
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
        Debug.Log("ResumeGame called");
    }

    public void TogglePause()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1) // Only toggle pause in Game scene
        {
            // Re-find pauseMenuCanvas if it's null
            if (pauseMenuCanvas == null)
            {
                Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
                Debug.Log("TogglePause: Total Canvases found: " + canvases.Length);
                foreach (Canvas canvas in canvases)
                {
                    Debug.Log("TogglePause: Found canvas: " + canvas.name + " at path: " + GetFullPath(canvas.gameObject));
                    if (canvas.name == "PauseMenuCanvas")
                    {
                        pauseMenuCanvas = canvas;
                        Debug.Log("Re-found PauseMenuCanvas in TogglePause");
                        break;
                    }
                }
            }

            if (pauseMenuCanvas == null)
            {
                Debug.LogError("TogglePause: PauseMenuCanvas is still null!");
                return;
            }

            IsPaused = !IsPaused;
            pauseMenuCanvas.gameObject.SetActive(IsPaused);
            Time.timeScale = IsPaused ? 0 : 1; // Freeze/unfreeze time
            Debug.Log("TogglePause: IsPaused = " + IsPaused + ", Canvas active = " + pauseMenuCanvas.gameObject.activeSelf + ", Time.timeScale = " + Time.timeScale);
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        IsPaused = false; // Reset pause state
        SceneManager.LoadScene("MainMenu"); // Returns to scene 0
        Debug.Log("Exiting to MainMenu"); // Debug transition
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null; // Clean up singleton on destroy
    }

    private string GetFullPath(GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }
}