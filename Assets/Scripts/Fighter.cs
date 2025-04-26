using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float speed = 5f;
    public int health = 100;
    public bool isPlayer1 = true;
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;
    public float projectileCooldown = 0.5f;
    private float lastProjectileTime;
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite attackSprite;
    private SpriteRenderer spriteRenderer;
    private float lastAttackTime;
    private Rigidbody2D rb;
    public float jumpVelocity = 10f;
    public float gravityScale = 3f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;
    public bool facingRight = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"Rigidbody2D missing on {gameObject.name}! Please add a Rigidbody2D component.");
            return;
        }
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (rb == null || MenuManager.IsPaused) return; // Skip if no Rigidbody2D or paused

        if (health <= 0) return; // Stop if dead

        // Handle movement
        Move();

        // Player 1 inputs
        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.W)) Jump();
            if (Input.GetKeyDown(KeyCode.Q)) Attack(10);
            if (Input.GetKeyDown(KeyCode.E) && Time.time - lastProjectileTime >= projectileCooldown)
            {
                ThrowProjectile();
                lastProjectileTime = Time.time;
            }
        }
        // Player 2 inputs
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
            if (Input.GetKeyDown(KeyCode.RightControl)) Attack(10);
            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time - lastProjectileTime >= projectileCooldown)
            {
                ThrowProjectile();
                lastProjectileTime = Time.time;
            }
        }
    }

    void Move()
    {
        float moveInput = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Update sprite based on movement
        if (moveInput != 0)
        {
            spriteRenderer.sprite = walkSprite;
            if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight)) Flip();
        }
        else if (Time.time - lastAttackTime > 0.5f)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    public void Attack(int damage)
    {
        spriteRenderer.sprite = attackSprite;
        lastAttackTime = Time.time;

        // Attack logic can be expanded (e.g., hitbox detection)
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log(gameObject.name + " is defeated!");
        }
    }

    void ThrowProjectile()
    {
        Vector2 spawnPosition = transform.position + (facingRight ? Vector3.right : Vector3.left) * 0.5f;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        ProjectileComponent projectileComp = projectile.GetComponent<ProjectileComponent>();
        projectileComp.Initialize(projectileSpeed, isPlayer1);
    }
}