using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    private float loadingTime = 3f; // Placeholder loading time (seconds)
    private float timer = 0f;

    void Start()
    {
        Text loadingText = GameObject.Find("LoadingText")?.GetComponent<Text>();
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
            SceneManager.LoadScene("Game");
        }
    }
}