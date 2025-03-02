using UnityEngine;

public abstract class Level
{
    public Sprite backgroundSprite; // Background image for this level
    public Vector2 player1SpawnPoint; // Spawn point for Player 1
    public Vector2 player2SpawnPoint; // Spawn point for Player 2
    public string levelName; // Name for identification

    public Level(Sprite background, Vector2 p1Spawn, Vector2 p2Spawn, string name)
    {
        backgroundSprite = background;
        player1SpawnPoint = p1Spawn;
        player2SpawnPoint = p2Spawn;
        levelName = name;
    }

    // Abstract method for level-specific initialization (e.g., power-ups)
    public abstract void InitializeLevel(GameObject backgroundObj);
}