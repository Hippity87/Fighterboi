using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public float damage = 10f; // Amount of damage this projectile deals
    private GameController gameController;

    void Start()
    {
        // Use FindFirstObjectByType instead of FindObjectOfType
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with is a Fighter
        Fighter fighter = other.GetComponent<Fighter>();
        if (fighter != null)
        {
            if (gameController != null)
            {
                // Determine which player this fighter is
                int player = fighter.isPlayer1 ? 1 : 2;
                gameController.TakeDamage(player, damage);
                Debug.Log($"Projectile hit {other.gameObject.name}, dealing {damage} damage to Player {player}");
            }
            else
            {
                Debug.LogWarning("GameController not found, cannot apply damage!");
            }
            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
}