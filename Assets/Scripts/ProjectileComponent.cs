using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public float damage = 10f; // Amount of damage this projectile deals
    public float lifetime = 5f; // How long the projectile exists before being destroyed

    private GameController gameController;
    private int shooterPlayerNumber; // The player number of the shooter (1 or 2)

    public void SetShooter(int playerNumber)
    {
        this.shooterPlayerNumber = playerNumber;
        Debug.Log($"SetShooter called: shooterPlayerNumber set to {shooterPlayerNumber}");
    }

    void Start()
    {
        // Use FindFirstObjectByType as per the original script
        gameController = FindFirstObjectByType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene!");
        }
        else
        {
            Debug.Log("GameController found successfully");
        }

        // Destroy the projectile after its lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Projectile collided with {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

        // Check if the object we collided with is a Fighter
        Fighter fighter = collision.gameObject.GetComponent<Fighter>();
        if (fighter == null)
        {
            Debug.Log($"No Fighter component found on {collision.gameObject.name}. Checking parent...");
            fighter = collision.gameObject.GetComponentInParent<Fighter>();
            if (fighter == null)
            {
                Debug.Log($"No Fighter component found on {collision.gameObject.name} or its parents");
                return; // Don't destroy the projectile unless it hits a fighter
            }
            else
            {
                Debug.Log($"Fighter component found on parent of {collision.gameObject.name}: {fighter.gameObject.name}");
            }
        }
        else
        {
            Debug.Log($"Fighter component found on {collision.gameObject.name}");
        }

        // Determine which player this fighter is
        int targetPlayerNumber = fighter.isPlayer1 ? 1 : 2;
        Debug.Log($"Target player number: {targetPlayerNumber}, Shooter player number: {shooterPlayerNumber}");

        // Only deal damage if the target is not the shooter
        if (targetPlayerNumber != shooterPlayerNumber)
        {
            if (gameController != null)
            {
                gameController.TakeDamage(targetPlayerNumber, damage);
                Debug.Log($"Projectile hit {collision.gameObject.name} (Player {targetPlayerNumber}), dealing {damage} damage");
            }
            else
            {
                Debug.LogWarning("GameController not found, cannot apply damage!");
            }
        }
        else
        {
            Debug.Log($"Projectile hit its shooter (Player {shooterPlayerNumber}), no damage applied");
        }
        Destroy(gameObject); // Destroy the projectile after hitting a fighter
    }
}