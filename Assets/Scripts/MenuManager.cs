using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;    // Assign in Inspector
    public Canvas pauseMenuCanvas;   // Assign in Inspector
    private bool isPaused = false;

    void Start()
    {
        // Show main menu at start
        if (mainMenuCanvas != null) mainMenuCanvas.gameObject.SetActive(true);
        Time.timeScale = 1; // Ensure game runs normally
    }

    void Update()
    {
        // Toggle pause menu with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowOptions()
    {
        Debug.Log("Options menu opened (functionality TBD)");
        // Add options UI logic later
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
        isPaused = !isPaused;
        pauseMenuCanvas.gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Freeze/unfreeze time
        if (!isPaused && mainMenuCanvas != null) mainMenuCanvas.gameObject.SetActive(false); // Hide main menu if returning
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}