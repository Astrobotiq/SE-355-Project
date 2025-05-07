using System.Collections;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Straight shoot damage received!");

            EnemyTakeDamage enemy = other.GetComponent<EnemyTakeDamage>();
            if (enemy != null)
            {
                enemy.takeDamage();
                Destroy(gameObject);
            }

        }
    }
}
