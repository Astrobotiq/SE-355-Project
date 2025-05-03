using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float maxDistance = 5f;
    public Vector2 direction;
    public ProjectileAttack attackScript;

    private Vector2 startPosition;
    private bool returning = false;
    private GameObject player;
    public float rotationSpeed = 360f;

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (!returning)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
            {
                returning = true;
            }
        }
        else
        {
            Vector2 playerPos = player.transform.position;
            Vector2 toPlayer = (playerPos - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(toPlayer * speed * Time.deltaTime);

            if (Vector2.Distance(playerPos, transform.position) < 0.5f)
            {
                attackScript.OnBoomerangReturned();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(40);
            }
        }
    }
}
