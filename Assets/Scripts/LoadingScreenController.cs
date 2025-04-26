using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this namespace for TextMeshPro

public class LoadingScreenController : MonoBehaviour
{
    private float loadingTime = 3f; // Placeholder loading time (seconds)
    private float timer = 0f;
    private TextMeshProUGUI loadingText; // Change to TextMeshProUGUI

    void Start()
    {
        // Find LoadingText, including inactive objects
        TextMeshProUGUI[] texts = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
        loadingText = null;
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.name == "LoadingText")
            {
                loadingText = text;
                break;
            }
        }

        if (loadingText == null)
        {
            Debug.LogError("LoadingText not found in LoadingScreen scene!");
            return;
        }
        loadingText.text = "Loading...";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= loadingTime)
        {
            Debug.Log("Loading complete, transitioning to Game scene");
            try
            {
                SceneManager.LoadScene("Game");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load Game scene: {e.Message}");
            }
        }
    }
}