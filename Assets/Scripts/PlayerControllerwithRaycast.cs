using UnityEngine;

public class PlayerControllerwithRaycast : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float maxFallSpeed = 20f;
    [SerializeField] float groundCheckDistance = 0.1f;

    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float timeToJumpApex = 0.4f;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] int maxAirJumps = 1;
    [SerializeField] float ceilingHitTolerance = 0.1f;

    [Header("Ray Settings")]
    [SerializeField] int horizontalRayCount = 4;
    [SerializeField] int verticalRayCount = 4;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float skinWidth = 0.02f;

    [Header("Advanced Jump")]
    [SerializeField] float runningJumpMultiplier = 1.2f;
    [SerializeField] float minJumpForce = 8f;

    [Header("Debug")]
    [SerializeField] bool showRays = true;
    [SerializeField] Color groundRayColor = Color.cyan;
    [SerializeField] Color collisionRayColor = Color.magenta;


    float gravity;
    float jumpVelocity;
    float currentVerticalVelocity;
    
    bool isGrounded;
    int airJumpCount;
    float lastGroundedTime;
    float lastJumpPressedTime;

    Rigidbody2D rb;
    BoxCollider2D coll;
    Vector2 velocity;
    Bounds bounds;
    float horizontalRaySpacing;
    float verticalRaySpacing;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rb.isKinematic = true;
        CalculatePhysicsParameters();
        CalculateRaySpacing();
    }

    void CalculatePhysicsParameters()
    {
        gravity = (2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
    }

    void Start()
    {
        currentVerticalVelocity = 0;
        velocity = Vector2.zero;
        isGrounded = false;
        lastJumpPressedTime = -jumpBufferTime;
        CheckGround();
    }
    void Update()
    {
        HandleInput();
        HandleTimers();
        HandleJump();
    }

    void HandleInput()
    {
        velocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        
        if(Input.GetButtonDown("Jump"))
        {
            lastJumpPressedTime = Time.time;
        }
    }

    void HandleTimers()
    {
        if(isGrounded)
        {
            lastGroundedTime = Time.time;
            airJumpCount = 0;
        }
    }

    void HandleJump()
    {
        bool canCoyoteJump = Time.time < lastGroundedTime + coyoteTime;
        bool hasJumpBuffer = Time.time < lastJumpPressedTime + jumpBufferTime;
        bool canNormalJump = isGrounded || canCoyoteJump;
        bool canAirJump = airJumpCount < maxAirJumps;

        if(hasJumpBuffer)
        {
            float calculatedJumpForce = jumpVelocity;
            
            if(Mathf.Abs(velocity.x) > 0.1f)
            {
                calculatedJumpForce *= runningJumpMultiplier;
            }
            else
            {
                calculatedJumpForce = Mathf.Max(calculatedJumpForce, minJumpForce);
            }

            if(canNormalJump)
            {
                currentVerticalVelocity = calculatedJumpForce;
                velocity.x *= 1.5f; 
                lastJumpPressedTime = 0;
                airJumpCount = 0;
            }
            else if(canAirJump)
            {
                currentVerticalVelocity = jumpVelocity;
                airJumpCount++;
                lastJumpPressedTime = 0;
            }
        }
        
    }

    void FixedUpdate()
    {
        ApplyGravity();
        UpdateBounds();
        CheckGround();
        CalculateMovement();
        Move();
    }

    void ApplyGravity()
    {
        if(!isGrounded)
        {
            currentVerticalVelocity -= gravity * Time.fixedDeltaTime;
            currentVerticalVelocity = Mathf.Max(currentVerticalVelocity, -maxFallSpeed);
        }
        else if(currentVerticalVelocity < 0)
        {
            currentVerticalVelocity = 0;
        }
    }

    void CheckGround()
    {
        float rayLength = groundCheckDistance + skinWidth;
        //bool wasGrounded = isGrounded;
        isGrounded = false;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = new Vector2(
                bounds.min.x + i * verticalRaySpacing,
                bounds.min.y - skinWidth           );

            RaycastHit2D hit = Physics2D.Raycast(
                rayOrigin, 
                Vector2.down, 
                rayLength, 
                collisionMask
            );

            if(hit)
            {
                isGrounded = true;
                break;
            }
            if (showRays)
            {
                Color rayColor = hit ? Color.green : groundRayColor;
                Debug.DrawRay(rayOrigin, Vector2.down * rayLength, rayColor);
            }

        }
    }

    void CalculateMovement()
    {
        // Yatay hareket
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x * Time.fixedDeltaTime) + skinWidth;

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? 
                new Vector2(bounds.min.x, bounds.min.y + i * horizontalRaySpacing) :
                new Vector2(bounds.max.x, bounds.min.y + i * horizontalRaySpacing);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if(hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX / Time.fixedDeltaTime;
                rayLength = hit.distance;
            }
            if (showRays)
            {
                Color rayColor = hit ? Color.red : collisionRayColor;
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, rayColor);
            }

        }

        // Dikey hareket
        float directionY = Mathf.Sign(currentVerticalVelocity);
        rayLength = Mathf.Abs(currentVerticalVelocity * Time.fixedDeltaTime) + skinWidth;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ?
                new Vector2(bounds.min.x + i * verticalRaySpacing, bounds.min.y - skinWidth) :
                new Vector2(bounds.min.x + i * verticalRaySpacing, bounds.max.y + skinWidth);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if(hit)
            {
                float distance = hit.distance - skinWidth;
                rayLength = hit.distance;
                currentVerticalVelocity = distance * directionY / Time.fixedDeltaTime;
                if (directionY > 0 && distance < ceilingHitTolerance)
                {
                    currentVerticalVelocity = 0f;
                }
            }
            if (showRays)
            {
                Color rayColor = hit ? Color.red : collisionRayColor;
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, rayColor);
            }

        }
    }

    void Move()
    {
        Vector2 finalVelocity = new Vector2(velocity.x, currentVerticalVelocity);
        rb.position += finalVelocity * Time.fixedDeltaTime;
    }

    void UpdateBounds()
    {
        bounds = coll.bounds;
        bounds.Expand(-skinWidth * 2);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    void CalculateRaySpacing()
    {
        bounds = coll.bounds;
        bounds.Expand(-skinWidth * 2);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
