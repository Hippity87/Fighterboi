using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;
    bool isGameOver = false;

    void Update()
    {
        if (!isGameOver) CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (player1.health <= 0) EndGame("Player 2 Wins!");
        else if (player2.health <= 0) EndGame("Player 1 Wins!");
    }

    void EndGame(string winner)
    {
        isGameOver = true;
        Debug.Log(winner);
    }
}