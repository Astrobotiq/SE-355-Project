using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Physics Settings")]
    public float moveSpeed = 2f;
    public float gravity = 30f;
    public float maxFallSpeed = 20f;
    public LayerMask collisionMask;
    public float skinWidth = 0.02f;

    protected Rigidbody2D rb;
    protected GameObject player;
    protected Vector2 velocity;
    protected float verticalVelocity = 0f;
    protected bool isGrounded;
    protected Bounds bounds;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        player = GameObject.FindWithTag("Player");
        FindObjectOfType<EnemyManager>()?.RegisterEnemy(this);
    }

    public virtual void CustomUpdate()
    {
        ApplyGravity();
        UpdateBounds();
        CheckGround();
    }

    protected void Move(Vector2 moveDir)
    {
        // verticalVelocity doğrudan y ekseninde kullanılıyor
        Vector2 finalVelocity = new Vector2(moveDir.x * moveSpeed, verticalVelocity);
        rb.position += finalVelocity * Time.deltaTime;
    }

    protected void ApplyGravity()
    {
        // Zıplama etkisini yok etmeden gravity uygula
        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, -maxFallSpeed);
        }
        else
        {
            // Yere çarptıysa zıplama bitti say, sadece aşağıya düşüyorsa sıfırla
            if (verticalVelocity < 0)
            {
                verticalVelocity = 0f;
            }
        }
    }


    protected void UpdateBounds()
    {
        bounds = GetComponent<Collider2D>().bounds;
        bounds.Expand(-skinWidth * 2);
    }

    protected void CheckGround()
    {
        float rayLength = 0.1f + skinWidth;
        isGrounded = false;

        Vector2 originLeft = new Vector2(bounds.min.x, bounds.min.y - skinWidth);
        Vector2 originRight = new Vector2(bounds.max.x, bounds.min.y - skinWidth);

        if (Physics2D.Raycast(originLeft, Vector2.down, rayLength, collisionMask) ||
            Physics2D.Raycast(originRight, Vector2.down, rayLength, collisionMask))
        {
            isGrounded = true;
        }

        // (İstersen gizli debug ray çizebilirim.)
    }

    protected void FlipTowards(Vector2 target)
    {
        bool shouldFlip = (target.x < transform.position.x && transform.localScale.x > 0)
                          || (target.x > transform.position.x && transform.localScale.x < 0);
        if (shouldFlip)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}