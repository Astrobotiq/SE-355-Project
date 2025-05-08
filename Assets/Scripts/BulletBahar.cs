using UnityEngine;

public class BulletBahar : MonoBehaviour
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

            HpBahar hp = other.GetComponent<HpBahar>();
            if (hp != null)
            {
                hp.StartCoroutine(hp.takeDamage());
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

}