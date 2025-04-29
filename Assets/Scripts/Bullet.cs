using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hasar ver veya başka bir şey yap
            Destroy(gameObject);
        }
    }
}