using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    private float speed;
    private bool fromPlayer1;
    private Rigidbody2D rb;

    public void Initialize(float projectileSpeed, bool isFromPlayer1)
    {
        speed = projectileSpeed;
        fromPlayer1 = isFromPlayer1;

        // Add Rigidbody2D if it doesn't exist
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0.5f; // Low gravity for slight arc
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Add collider if it doesn't exist
        if (GetComponent<Collider2D>() == null)
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.radius = 0.2f;
            collider.isTrigger = true; // Use trigger for projectiles
        }

        // Set initial velocity
        rb.linearVelocity = new Vector2(speed, 1f); // Add slight upward motion
    }

    void Update()
    {
        // Destroy projectile if it goes too far off-screen
        if (Mathf.Abs(transform.position.x) > 20 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile hit a fighter
        Fighter fighter = other.GetComponent<Fighter>();

        if (fighter != null && fighter.isPlayer1 != fromPlayer1)
        {
            // Deal damage to the opponent
            fighter.TakeDamage(5);
            Debug.Log("Projectile hit " + (fighter.isPlayer1 ? "Player 1" : "Player 2") + "!");

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}