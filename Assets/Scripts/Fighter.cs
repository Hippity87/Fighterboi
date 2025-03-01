// using UnityEngine;

// public class Fighter : MonoBehaviour
// {
//     public float speed = 5f;
//     public int health = 100;
//     public bool isPlayer1 = true;

//     // Add your sprites here—drag them in Unity Inspector
//     public Sprite idleSprite;
//     public Sprite walkSprite;
//     public Sprite attackSprite;
//     private SpriteRenderer spriteRenderer;
//     private float lastAttackTime; // Track when attack started
//     private Rigidbody2D rb; // For physics/jumping

//     // Jump settings
//     public float jumpVelocity = 10f; // Default jump speed
//     public float gravityScale = 3f; // Default gravity multiplier
//     public LayerMask groundLayer; // Assign "Ground" layer in Inspector
//     public float groundCheckDistance = 0.1f; // How far to check for ground

//     void Start()
//     {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         rb = GetComponent<Rigidbody2D>(); // Get or add Rigidbody2D
//         if (spriteRenderer == null)
//         {
//             Debug.LogError("No SpriteRenderer on " + gameObject.name + "! Please add one.");
//             return;
//         }
//         if (rb == null)
//         {
//             rb = gameObject.AddComponent<Rigidbody2D>(); // Add if missing
//             rb.gravityScale = gravityScale; // Set gravity
//             rb.freezeRotation = true; // Prevent spinning
//         }

//         // Assign this GameObject to the "Player" layer
//         gameObject.layer = LayerMask.NameToLayer("Player");
//         spriteRenderer.sprite = idleSprite;
//     }

//     void Update()
//     {
//         Move();
//         // Jump on Spacebar only if on ground
//         if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
//         {
//             Debug.Log("Spacebar pressed for jump!");
//             Jump();
//         }
//         // Attack on 'V'
//         if (Input.GetKeyDown(KeyCode.V))
//         {
//             Debug.Log("V pressed!");
//             Attack(10);
//         }
//         // Keep attack sprite while 'V' is held
//         if (Input.GetKey(KeyCode.V))
//         {
//             spriteRenderer.sprite = attackSprite;
//         }
//         // Reset to idle 100ms after 'V' is released
//         if (Input.GetKeyUp(KeyCode.V) && Time.time >= lastAttackTime + 0.1f)
//         {
//             spriteRenderer.sprite = idleSprite;
//         }
//     }

//     void Move()
//     {
//         float moveX = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("HorizontalP2");
//         rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y); // Use Rigidbody2D for movement

//         // Only change sprite if not attacking
//         if (!Input.GetKey(KeyCode.V))
//         {
//             if (moveX != 0) spriteRenderer.sprite = walkSprite;
//             else spriteRenderer.sprite = idleSprite;
//         }
//     }

//     void Jump()
//     {
//         rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity); // Apply jump
//         Debug.Log("Jumped with velocity: " + jumpVelocity);
//     }

//     bool IsGrounded()
//     {
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
//         return hit.collider != null;
//     }

//     public void Attack(int damage)
//     {
//         spriteRenderer.sprite = attackSprite;
//         lastAttackTime = Time.time;
//         Debug.Log("Attacking for " + damage + " damage!");
//     }

//     public void TakeDamage(int damage)
//     {
//         health -= damage;
//         if (health <= 0) Debug.Log("KO!");
//     }
// }


using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float speed = 5f;
    public int health = 100;
    public bool isPlayer1 = true;

    // Add your sprites here—drag them in Unity Inspector
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite attackSprite;
    private SpriteRenderer spriteRenderer;
    private float lastAttackTime; // Track when attack started
    private Rigidbody2D rb; // For physics/jumping

    // Jump settings
    public float jumpVelocity = 10f; // Default jump speed
    public float gravityScale = 3f; // Default gravity multiplier
    public LayerMask groundLayer; // Assign "Ground" layer in Inspector
    public float groundCheckDistance = 0.5f; // How far to check for ground

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Get or add Rigidbody2D
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer on " + gameObject.name + "! Please add one.");
            return;
        }
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>(); // Add if missing
            rb.gravityScale = gravityScale; // Set gravity
            rb.freezeRotation = true; // Prevent spinning
        }
        // Assign this GameObject to the "Player" layer
        gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar pressed!");
            bool isOnGround = IsGrounded();
            Debug.Log("IsGrounded: " + isOnGround);

            if (isOnGround)
            {
                Debug.Log("Jumping!");
                Jump();
            }
            else
            {
                Debug.Log("Can't jump - not on ground!");
            }
        }







        // Attack on 'V'
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("V pressed!");
            Attack(10);
        }
        // Keep attack sprite while 'V' is held
        if (Input.GetKey(KeyCode.V))
        {
            spriteRenderer.sprite = attackSprite;
        }
        // Reset to idle 100ms after 'V' is released
        if (Input.GetKeyUp(KeyCode.V) && Time.time >= lastAttackTime + 0.1f)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void Move()
    {
        float moveX = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("HorizontalP2");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y); // Use Rigidbody2D for movement

        // Flip sprite based on movement direction
        if (moveX != 0)
        {
            // If moving right, don't flip (original facing direction)
            // If moving left, flip the sprite
            spriteRenderer.flipX = (moveX < 0);
        }

        // Only change sprite if not attacking
        if (!Input.GetKey(KeyCode.V))
        {
            if (moveX != 0) spriteRenderer.sprite = walkSprite;
            else spriteRenderer.sprite = idleSprite;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity); // Apply jump
        Debug.Log("Jumped with velocity: " + jumpVelocity);
    }

    bool IsGrounded()
    {
        // Check for ground in a small radius below the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -0.5f, 0), 0.2f, groundLayer);

        // Visualize the ground check area in the Scene view
        Debug.DrawRay(transform.position, Vector3.down * 0.7f, Color.yellow);

        // If we found any ground colliders, we're grounded
        return colliders.Length > 0;
    }

    public void Attack(int damage)
    {
        spriteRenderer.sprite = attackSprite;
        lastAttackTime = Time.time;
        Debug.Log("Attacking for " + damage + " damage!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Debug.Log("KO!");
    }
}