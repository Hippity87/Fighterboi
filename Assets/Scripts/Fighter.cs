using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public float projectileCooldown = 0.5f;
    public bool isPlayer1 = true;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private float lastProjectileTime;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalSpawnPointLocalPosition; // Store the original local position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on Fighter!");
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on Fighter!");
        }
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"Projectile Prefab not assigned on {gameObject.name}! Projectiles will not spawn.");
        }
        if (projectileSpawnPoint == null)
        {
            Debug.LogWarning($"Projectile Spawn Point not assigned on {gameObject.name}! Projectiles will spawn at the fighter's position.");
        }
        else
        {
            // Store the original local position of the spawn point
            originalSpawnPointLocalPosition = projectileSpawnPoint.localPosition;
        }
    }

    void Update()
    {
        if (isPlayer1)
        {
            horizontalInput = 0f;
            if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastProjectileTime >= projectileCooldown)
            {
                ThrowProjectile();
            }
        }
        else
        {
            horizontalInput = 0f;
            if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time - lastProjectileTime >= projectileCooldown)
            {
                ThrowProjectile();
            }
        }

        if (horizontalInput != 0f)
        {
            spriteRenderer.flipX = horizontalInput < 0;
            // Adjust the projectile spawn point's position based on facing direction
            if (projectileSpawnPoint != null)
            {
                Vector3 newLocalPosition = originalSpawnPointLocalPosition;
                newLocalPosition.x = spriteRenderer.flipX ? -Mathf.Abs(originalSpawnPointLocalPosition.x) : Mathf.Abs(originalSpawnPointLocalPosition.x);
                projectileSpawnPoint.localPosition = newLocalPosition;
            }
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.x = horizontalInput * moveSpeed;
            rb.linearVelocity = velocity;
        }
    }

    void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            Debug.Log($"{gameObject.name} jumped");
        }
    }

    void ThrowProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"Cannot throw projectile: Projectile Prefab is null on {gameObject.name}!");
            return;
        }

        Vector3 spawnPosition = (projectileSpawnPoint != null) ? projectileSpawnPoint.position : transform.position;
        Debug.Log($"{gameObject.name} is throwing a projectile at position {spawnPosition}");
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            float direction = spriteRenderer.flipX ? -1f : 1f;
            projectileRb.linearVelocity = new Vector2(direction * projectileSpeed, 0f);
        }
        else
        {
            Debug.LogWarning($"Projectile {projectile.name} does not have a Rigidbody2D!");
        }
        lastProjectileTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} (Tag: {collision.gameObject.tag})");
        try
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                Debug.Log($"{gameObject.name} is grounded");
            }
        }
        catch (UnityException e)
        {
            Debug.LogWarning($"Error comparing tag 'Ground' on {collision.gameObject.name}: {e.Message}. Please ensure the 'Ground' tag is defined in the project.");
            if (collision.gameObject.name.ToLower().Contains("ground") || collision.gameObject.name.ToLower().Contains("floor"))
            {
                isGrounded = true;
                Debug.Log($"{gameObject.name} landed on {collision.gameObject.name} (using name-based fallback)");
            }
        }
    }
}