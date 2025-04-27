using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameController gameController;
    private bool isGameOver = false;

    void Start()
    {
        // Use FindFirstObjectByType instead of FindObjectOfType
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene!");
        }
    }

    void Update()
    {
        if (!isGameOver && gameController != null)
        {
            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        float p1Health = gameController.GetPlayerHealth(1);
        float p2Health = gameController.GetPlayerHealth(2);

        if (p1Health <= 0)
        {
            EndGame("Player 2 Wins!");
        }
        else if (p2Health <= 0)
        {
            EndGame("Player 1 Wins!");
        }
    }

    void EndGame(string winner)
    {
        isGameOver = true;
        Debug.Log(winner);
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenu");
    }
}