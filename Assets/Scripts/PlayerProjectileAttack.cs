using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public enum WeaponMode { Normal, Boomerang }
    public WeaponMode currentMode = WeaponMode.Normal;

    public GameObject normalProjectilePrefab;
    public GameObject boomerangProjectilePrefab;

    public Transform firePoint;
    public float projectileSpeed = 10f;

    private GameObject activeBoomerang;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentMode = (currentMode == WeaponMode.Normal) ? WeaponMode.Boomerang : WeaponMode.Normal;
            Debug.Log("Mode changed to: " + currentMode);
        }
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.Q))
            direction = (Vector2.up + Vector2.left).normalized;
        else if (Input.GetKey(KeyCode.E))
            direction = (Vector2.up + Vector2.right).normalized;
        else if (Input.GetKey(KeyCode.W))
            direction = Vector2.up;
        else if (Input.GetKey(KeyCode.F))
            direction = firePoint.right;

        if (direction != Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
            {
                if (currentMode == WeaponMode.Normal)
                {
                    FireNormal(direction);
                }
                else if (currentMode == WeaponMode.Boomerang && activeBoomerang == null)
                {
                    FireBoomerang(direction);
                }
            }
        }
    }

    void FireNormal(Vector2 direction)
    {
        GameObject projectile = Instantiate(normalProjectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }

    void FireBoomerang(Vector2 direction)
    {
        activeBoomerang = Instantiate(boomerangProjectilePrefab, firePoint.position, Quaternion.identity);
        BoomerangProjectile boom = activeBoomerang.GetComponent<BoomerangProjectile>();
        if (boom != null)
        {
            boom.direction = direction;
            boom.attackScript = this;
        }
    }

    public void OnBoomerangReturned()
    {
        activeBoomerang = null;
    }
}
