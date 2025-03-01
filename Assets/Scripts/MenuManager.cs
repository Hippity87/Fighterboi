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
        if (mainMenuCanvas != null) mainMenuCanvas.gameObject.SetActive(true);
        Time.timeScale = 1; // Ensure game runs normally
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Loads next scene in build profile
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
        isPaused = !isPaused;
        pauseMenuCanvas.gameObject.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Freeze/unfreeze time
        if (!isPaused && mainMenuCanvas != null) mainMenuCanvas.gameObject.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // Returns to first scene in build profile
    }
}

//useless comment force recompile