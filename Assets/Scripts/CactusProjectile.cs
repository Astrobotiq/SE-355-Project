using UnityEngine;

public class CactusProjectile : MonoBehaviour
{
    public float lifetime = 5f;
    public float rotationSpeed = 360f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
            Destroy(gameObject);
        }
    }
}
