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
        // Player 1 starts facing right, Player 2 starts facing left
        facingRight = isPlayer1;
        spriteRenderer.flipX = !facingRight;
    }


    void Update()
    {
        Move();

        // Jump based on player
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.Space)) ||
            (!isPlayer1 && Input.GetKeyDown(KeyCode.RightControl)))
        {
            bool isOnGround = IsGrounded();
            Debug.Log("Is Grounded: " + isOnGround);
            if (isOnGround)
            {
                Debug.Log((isPlayer1 ? "Player 1" : "Player 2") + " jumped!");
                Jump();
            }
        }

        // Attack based on player
        if ((isPlayer1 && Input.GetKeyDown(KeyCode.V)) ||
            (!isPlayer1 && Input.GetKeyDown(KeyCode.RightShift)))
        {
            Debug.Log((isPlayer1 ? "Player 1" : "Player 2") + " attacking!");
            Attack(10);
        }

        // Keep attack sprite while attack key is held
        if ((isPlayer1 && Input.GetKey(KeyCode.V)) ||
            (!isPlayer1 && Input.GetKey(KeyCode.RightShift)))
        {
            spriteRenderer.sprite = attackSprite;
        }

        // Reset to idle after attack key is released
        if ((isPlayer1 && Input.GetKeyUp(KeyCode.V) && Time.time >= lastAttackTime + 0.1f) ||
            (!isPlayer1 && Input.GetKeyUp(KeyCode.RightShift) && Time.time >= lastAttackTime + 0.1f))
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    // Add a new field to determine the initial facing direction
    public bool facingRight = true;

    void Move()
    {
        // Player 1: Uses Horizontal axis (arrow keys/WASD)
        // Player 2: Will use J/L keys for left/right
        float moveX = 0;

        if (isPlayer1)
        {
            moveX = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            // For Player 2, use different keys
            if (Input.GetKey(KeyCode.J)) moveX = -1;
            if (Input.GetKey(KeyCode.L)) moveX = 1;
        }

        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);

        // Handle sprite direction - flip only when needed
        if (moveX > 0 && !facingRight) // Moving right but facing left
        {
            Flip();
        }
        else if (moveX < 0 && facingRight) // Moving left but facing right
        {
            Flip();
        }

        // Only change sprite if not attacking
        if ((isPlayer1 && !Input.GetKey(KeyCode.V)) ||
            (!isPlayer1 && !Input.GetKey(KeyCode.RightShift)))
        {
            if (moveX != 0) spriteRenderer.sprite = walkSprite;
            else spriteRenderer.sprite = idleSprite;
        }
    }

    // New method to flip the character
    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }




    // void Update()
    // {
    //     Move();

    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Debug.Log("Spacebar pressed!");
    //         bool isOnGround = IsGrounded();
    //         Debug.Log("IsGrounded: " + isOnGround);

    //         if (isOnGround)
    //         {
    //             Debug.Log("Jumping!");
    //             Jump();
    //         }
    //         else
    //         {
    //             Debug.Log("Can't jump - not on ground!");
    //         }
    //     }







    //     // Attack on 'V'
    //     if (Input.GetKeyDown(KeyCode.V))
    //     {
    //         Debug.Log("V pressed!");
    //         Attack(10);
    //     }
    //     // Keep attack sprite while 'V' is held
    //     if (Input.GetKey(KeyCode.V))
    //     {
    //         spriteRenderer.sprite = attackSprite;
    //     }
    //     // Reset to idle 100ms after 'V' is released
    //     if (Input.GetKeyUp(KeyCode.V) && Time.time >= lastAttackTime + 0.1f)
    //     {
    //         spriteRenderer.sprite = idleSprite;
    //     }
    // }

    // void Move()
    // {
    //     float moveX = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("HorizontalP2");
    //     rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y); // Use Rigidbody2D for movement

    //     // Flip sprite based on movement direction
    //     if (moveX != 0)
    //     {
    //         // If moving right, don't flip (original facing direction)
    //         // If moving left, flip the sprite
    //         spriteRenderer.flipX = (moveX < 0);
    //     }

    //     // Only change sprite if not attacking
    //     if (!Input.GetKey(KeyCode.V))
    //     {
    //         if (moveX != 0) spriteRenderer.sprite = walkSprite;
    //         else spriteRenderer.sprite = idleSprite;
    //     }
    // }

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