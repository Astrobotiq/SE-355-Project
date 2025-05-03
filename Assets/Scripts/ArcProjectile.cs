using System.Collections;
using UnityEngine;

public class ArcProjectile : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float delayBeforeExplosion = 1f;
    public ParticleSystem explosionEffect;

    private void Start()
    {
        if (explosionEffect != null)
        {
            explosionEffect.gameObject.SetActive(false);
        }

        StartCoroutine(ExplosionDelay());
    }
    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(delayBeforeExplosion);

        Explode();
    }

    void Explode()
    {
        ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        effect.gameObject.SetActive(true);
        effect.Play();
        Destroy(effect.gameObject, effect.main.duration);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Explosion damage received!");
                EnemyTakeDamage enemy = hit.GetComponent<EnemyTakeDamage>();
                if (enemy != null)
                {
                    enemy.takeDamage();
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
