using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    private float speed;
    private bool fromPlayer1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float projectileSpeed, bool isFromPlayer1)
    {
        speed = projectileSpeed;
        fromPlayer1 = isFromPlayer1;
        rb.linearVelocity = (fromPlayer1 ? Vector2.right : Vector2.left) * speed;
    }

    void Update()
    {
        if (MenuManager.IsPaused) return; // Skip logic if paused

        // Destroy projectile if off-screen
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (MenuManager.IsPaused) return; // Skip collision if paused

        Fighter fighter = other.GetComponent<Fighter>();
        if (fighter != null && fighter.isPlayer1 != fromPlayer1)
        {
            fighter.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}